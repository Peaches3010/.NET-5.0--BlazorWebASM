using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.API.Data;
using Todolist.API.Entities;
using TodoList.Share;

namespace Todolist.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoListDbContext _context;
        public UserRepository(TodoListDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
