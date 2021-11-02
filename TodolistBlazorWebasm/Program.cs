using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodolistBlazorWebasm.Services;

namespace TodolistBlazorWebasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            builder.Services.AddScoped(sp => new HttpClient 
            { 
                BaseAddress = new Uri(builder.Configuration["BackendApiUrl"]) 
            });
            builder.Services.AddAuthorizationCore();
            builder.Services.AddTransient<ITaskApiClient, TaskApiClient>();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddTransient<IUserApiClient, UserApiClient>();
            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredLocalStorage();
            await builder.Build().RunAsync();
        }
    }
}
