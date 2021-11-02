
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Todolist.API.Entities;
using TodoList.Share;

namespace Todolist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public LoginController(IConfiguration configuration, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login ([FromBody] LoginRequest login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            var user = await _userManager.FindByNameAsync(login.UserName);
            if(!result.Succeeded)
                return BadRequest(new LoginResponse { Successfull = false, Error="Username or password are invalid"});

            var claims = new[]
            {
              new Claim ( ClaimTypes.Name , login.UserName),
              new Claim ("UserId",user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials:creds
                );

            return Ok(new LoginResponse { Successfull = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
