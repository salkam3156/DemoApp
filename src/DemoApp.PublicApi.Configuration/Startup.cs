using DemoApp.Infrastructure.Repositories.DbContexts;
using DemoApp.PublicApi.Configuration.Configuration;
using DemoApp.PublicApi.Configuration.Loaders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DemoApp.PublicApi.Configuration
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        
        public Startup(IConfiguration configuration)
            => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DomainContext>(opt => opt.UseInMemoryDatabase(databaseName: "Domain"));

            services
                .AddOptions()
                .Configure<Plugins>(_configuration);

            var pluginsConfiguration = _configuration.Get<Plugins>();
            services.AddControllersFromExternalAssembly(pluginsConfiguration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = pluginsConfiguration.ControllersAssembly, Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                var pluginsConfiguration = _configuration.Get<Plugins>();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{pluginsConfiguration.ControllersAssembly} v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
