using DemoApp.ApplicationCore.RepositoryContracts;
using DemoApp.Infrastructure.ClientNotifiers;
using DemoApp.Infrastructure.Repositories.DbContexts;
using DemoApp.Infrastructure.Repositories.Implementations;
using DemoApp.PublicApi.Configuration.Configuration;
using DemoApp.PublicApi.Configuration.Loaders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using DemoApp.ApplicationServices.MessagingContracts;

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

            services.AddSignalR(cfg =>
            {
                cfg.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
                cfg.HandshakeTimeout = TimeSpan.FromSeconds(30);
                cfg.KeepAliveInterval = TimeSpan.FromSeconds(30);
            });

            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<INotifier, NotificationGateway>();


            var pluginsConfiguration = _configuration.Get<Plugins>();
            services.AddControllersFromExternalAssembly(pluginsConfiguration);
            services.AddApplicationFeatures(pluginsConfiguration);

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
                endpoints.MapHub<SignalRNotificationHub>("/notififcations", opt => opt.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling);
                endpoints.MapControllers();
            });
        }
    }
}
