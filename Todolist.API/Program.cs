using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Todolist.API.Data;
using Todolist.API.Extensions;
using Microsoft.Extensions.DependencyInjection;
namespace Todolist.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.MigrateDbContext<TodoListDbContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<TodoListDbContextSeed>>();
                new TodoListDbContextSeed().SeedAsync(context, logger).Wait();
            });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
