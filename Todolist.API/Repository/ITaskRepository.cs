
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todolist.API.Entities;

namespace Todolist.API.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ToDoTask>> GetTaskList();

        Task<ToDoTask> Create(ToDoTask toDoTask);

        Task<ToDoTask> Update(ToDoTask toDoTask);

        Task<ToDoTask> Delete(ToDoTask toDoTask);

        Task<ToDoTask> GetTaskById(Guid id);

    }
}
