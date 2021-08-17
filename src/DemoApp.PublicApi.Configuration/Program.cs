using DemoApp.Infrastructure.Repositories.DbContexts;
using DemoApp.PublicApi.Configuration.Development;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace DemoApp.PublicApi.Configuration
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();

#if DEBUG
            using var dbContext = hostBuilder.Services
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<DomainContext>();

            await InMemoryDbInitializer.Initialize(dbContext);
#endif

            await hostBuilder.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
