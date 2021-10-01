using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.API.Data;
using Todolist.API.Entities;

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

        public async Task<IEnumerable<TodoTask>> GetTaskList()
         => await _context.ToDoTasks.ToListAsync();

        public async Task<TodoTask> Update(TodoTask toDoTask)
        {
             _context.ToDoTasks.Update(toDoTask);
            await _context.SaveChangesAsync();
            return toDoTask; 
        }
    }
}
