using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Share;

namespace TodolistBlazorWebasm.Services
{
    public interface IAuthServices
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task Logout();
    }
}
