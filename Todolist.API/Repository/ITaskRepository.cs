
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todolist.API.Entities;
using TodoList.Share;
using TodoList.Share.SeedWork;

namespace Todolist.API.Repository
{
    public interface ITaskRepository
    {
        Task<PagedList<TodoTask>> GetTaskList(TaskListSearch taskListSearch );

        Task<PagedList<TodoTask>> GetTaskByUserId(Guid userId,TaskListSearch taskListSearch);

        Task<TodoTask> Create(TodoTask toDoTask);

        Task<TodoTask> Update(TodoTask toDoTask);

        Task<TodoTask> Delete(TodoTask toDoTask);

        Task<TodoTask> GetTaskById(Guid id);

    }
}
