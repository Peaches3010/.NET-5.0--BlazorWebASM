using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.API.Repository;
using TodoList.Share;

namespace Todolist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _usertRepository;

        public UserController(IUserRepository userRepository)
        {
            _usertRepository = userRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usertRepository.GetUserList();

            var assignees = users.Select(x => new AssigneeDto()
            {
                Id = x.Id,
                FullName = x.FirstName + " "+ x.LastName
            });

            return Ok(assignees);
        }
    }
}
