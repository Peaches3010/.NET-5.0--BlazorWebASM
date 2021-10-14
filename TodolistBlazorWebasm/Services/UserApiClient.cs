using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoList.Share;

namespace TodolistBlazorWebasm.Services
{
    public class UserApiClient : IUserApiClient
    {
        public HttpClient _httpClient;

        public UserApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AssigneeDto>> GetUserList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AssigneeDto>>($"api/User");

            return result;
        }
    }
}
