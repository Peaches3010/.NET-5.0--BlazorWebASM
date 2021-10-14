using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.API.Data;
using Todolist.API.Entities;
using TodoList.Share;
using TodoList.Share.SeedWork;

namespace Todolist.API.Repository
{
    public class TaskRepository : ITaskRepository
    {

        private readonly TodoListDbContext _context;

        public TaskRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task<TodoTask> Create(TodoTask toDoTask)
        {
            await _context.ToDoTasks.AddAsync(toDoTask);
            await _context.SaveChangesAsync();
            return toDoTask;
        }

        public async Task<TodoTask> Delete(TodoTask toDoTask)
        {
             _context.ToDoTasks.Remove(toDoTask);
            await _context.SaveChangesAsync();
            return toDoTask;

        }

        public async Task<TodoTask> GetTaskById(Guid id)
         => await _context.ToDoTasks.FindAsync(id);

        public async Task<PagedList<TodoTask>> GetTaskByUserId(Guid userId, TaskListSearch taskListSearch)
        {
            var query = _context.ToDoTasks
                                .Where(x=>x.AssigneeId== userId)
                                .Include(x => x.Assignee)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(taskListSearch.Name))
                query = query.Where(x => x.Name.Contains(taskListSearch.Name));

            if (taskListSearch.AssigneeId.HasValue)
                query = query.Where(x => x.AssigneeId == taskListSearch.AssigneeId.Value);

            if (taskListSearch.Priority.HasValue)
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.CreatedDate)
                             .Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
                             .Take(taskListSearch.PageSize)
                             .ToListAsync();
            return new PagedList<TodoTask>(data, count, taskListSearch.PageNumber, taskListSearch.PageSize);

        }

        public async Task<PagedList<TodoTask>> GetTaskList(TaskListSearch taskListSearch)
        {
            var query = _context.ToDoTasks.Include(x => x.Assignee).AsQueryable();

            if (!string.IsNullOrEmpty(taskListSearch.Name))
                query = query.Where(x => x.Name.Contains(taskListSearch.Name));

            if(taskListSearch.AssigneeId.HasValue)
                query = query.Where(x => x.AssigneeId == taskListSearch.AssigneeId.Value);

            if (taskListSearch.Priority.HasValue)
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.CreatedDate)
                             .Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
                             .Take(taskListSearch.PageSize)
                             .ToListAsync();
            return new PagedList<TodoTask>(data,count,taskListSearch.PageNumber,taskListSearch.PageSize);
              
        }
         

        public async Task<TodoTask> Update(TodoTask toDoTask)
        {
             _context.ToDoTasks.Update(toDoTask);
            await _context.SaveChangesAsync();
            return toDoTask; 
        }
    }
}
