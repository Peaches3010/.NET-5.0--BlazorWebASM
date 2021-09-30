using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.API.Entities;

using Todolist.Share.Enum;

namespace Todolist.API.Data
{
    public class TodoListDbContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public async Task SeedAsync(TodoListDbContext context, ILogger<TodoListDbContextSeed> logger)
        {
           
            if(!context.ToDoTasks.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Duc",
                    LastName = "Nhat",
                    Email = "daoducnhat3010@gmail.com",
                    UserName = "Admin"
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Abc@123");
                context.Users.Add(user);

            }
            if (!context.ToDoTasks.Any())
            {
                context.ToDoTasks.Add(new Entities.ToDoTask()
                {
                    Id = Guid.NewGuid(),
                    Name= "Task 1",
                    CreatedDate = DateTime.Now,
                    Priority = Priority.High,
                    Status = Status.Open
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
