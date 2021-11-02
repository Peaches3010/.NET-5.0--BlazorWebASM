using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.API.Entities;
using TodoList.Share;

namespace Todolist.API.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();
    }
}
