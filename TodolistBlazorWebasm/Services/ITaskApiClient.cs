 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Share;
using TodoList.Share.SeedWork;

namespace TodolistBlazorWebasm.Services
{
   public interface ITaskApiClient
    {
        Task<PagedList<TodoTaskDto>> GetTaskList(TaskListSearch taskListSearch);

        Task<PagedList<TodoTaskDto>> GetMyTask(TaskListSearch taskListSearch);

        Task<TodoTaskDto> GetTaskDetail(string id);

        Task<bool> CreateTask(TodoTaskCreateRequest request);

        Task<bool> UpdateTask(Guid Id, TodoTaskUpdateRequest request);

        Task<bool> AssignTask(Guid Id, AssigneeTaskRequest request);


        Task<bool> DeleteTask(Guid Id);
    }
}
