
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todolist.API.Entities;

namespace Todolist.API.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TodoTask>> GetTaskList();

        Task<TodoTask> Create(TodoTask toDoTask);

        Task<TodoTask> Update(TodoTask toDoTask);

        Task<TodoTask> Delete(TodoTask toDoTask);

        Task<TodoTask> GetTaskById(Guid id);

    }
}
