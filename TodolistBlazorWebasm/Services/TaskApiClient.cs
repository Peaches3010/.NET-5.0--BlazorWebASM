using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoList.Share;
using TodoList.Share.SeedWork;

namespace TodolistBlazorWebasm.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        public HttpClient _httpClient;

        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AssignTask(Guid Id, AssigneeTaskRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Task/{Id}/assign", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> CreateTask(TodoTaskCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Task", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTask(Guid Id)
        {
            var result = await _httpClient.DeleteAsync($"/api/Task/{Id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<PagedList<TodoTaskDto>> GetMyTask(TaskListSearch taskListSearch)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
            };

            string url = QueryHelpers.AddQueryString("/api/Task/me", queryStringParam);

            var result = await _httpClient.GetFromJsonAsync<PagedList<TodoTaskDto>>(url);

            return result;
        }

        public async Task<TodoTaskDto> GetTaskDetail(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<TodoTaskDto>($"api/Task/{id}");

            return result;
        }

        public async Task<PagedList<TodoTaskDto>> GetTaskList(TaskListSearch taskListSearch)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
            };

           string url = QueryHelpers.AddQueryString("/api/Task", queryStringParam);

           var result = await _httpClient.GetFromJsonAsync<PagedList<TodoTaskDto>>(url);
            
           return result;
        }

        public async Task<bool> UpdateTask(Guid Id, TodoTaskUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Task/{Id}", request);
            return result.IsSuccessStatusCode;
        }
    }
}
