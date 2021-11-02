using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.API.Entities;
using Todolist.API.Extensions;
using Todolist.API.Repository;
using Todolist.Share.Enum;
using TodoList.Share;
using TodoList.Share.SeedWork;

namespace Todolist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
      
        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
            
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetAll([FromQuery] TaskListSearch taskListSearch)
        {
            var userId = User.GetUserId();
            var pagedList = await _taskRepository.GetTaskByUserId(Guid.Parse(userId),taskListSearch);
            var toDoTaskDtos = pagedList.Items.Select(x => new TodoTaskDto()
            {
                Status = x.Status,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                CreatedDate = x.CreatedDate,
                Priority = x.Priority,
                Id = x.Id,
                AssigneeName = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A"
            }); 

            return Ok(new PagedList<TodoTaskDto>(toDoTaskDtos.ToList(),
                                                        pagedList.MetaData.TotalCount,
                                                        pagedList.MetaData.CurrentPage,
                                                        pagedList.MetaData.PageSize));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TodoTaskCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newtask = await _taskRepository.Create(new TodoTask()
            {
                Id = request.Id,
                Name = request.Name,
                CreatedDate = DateTime.Now,
                Status = Status.Open,
                Priority = request.Priority.HasValue ? request.Priority.Value : Priority.Low
            });

            return CreatedAtAction(nameof(GetTaskById), new { taskId = newtask.Id }, newtask);

        }
        [HttpGet]
        [Route("{taskId}")]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);

            if (task == null)
                return NotFound($"{taskId} is not found");

            return Ok(new TodoTaskDto()
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Id,
                AssigneeId = task.AssigneeId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate
            });
        }

        [HttpPut]
        [Route("{taskId}")]
        public async Task<IActionResult> UpdateTask(Guid taskId, [FromBody]TodoTaskUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _taskRepository.GetTaskById(taskId);
            if (taskFromDb == null)
            {
                return NotFound($"{taskId} is not found");
            }

            taskFromDb.Name = request.Name;
            taskFromDb.Priority = request.Priority;



            var taskResult = await _taskRepository.Update(taskFromDb);

            return Ok(new TodoTaskDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate
            });

        }

        [HttpDelete]
        [Route("{taskId}")]
        public async Task<IActionResult> DeleteTask ([FromRoute]Guid taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null) return NotFound($"{taskId} is not found");

            await _taskRepository.Delete(task);
            return Ok(new TodoTaskDto()
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Id,
                AssigneeId = task.AssigneeId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate
            });
        }

        [HttpPut]
        [Route("{id}/assign")]
        public async Task<IActionResult> AssignTask(Guid Id,[FromBody] AssigneeTaskRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _taskRepository.GetTaskById(Id);
            if (taskFromDb == null)
            {
                return NotFound($"{Id} is not found");
            }

            taskFromDb.AssigneeId = request.UserId.Value;


            var taskResult = await _taskRepository.Update(taskFromDb);

            return Ok(new TodoTaskDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate
            });


        }
    }
}
