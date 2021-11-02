using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Todolist.API.Entities;

namespace Todolist.API.Data
{
    public class TodoListDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public TodoListDbContext( DbContextOptions<TodoListDbContext> options) : base(options)
        {
        }
        public DbSet<TodoTask> ToDoTasks { set; get; }

    }
}
