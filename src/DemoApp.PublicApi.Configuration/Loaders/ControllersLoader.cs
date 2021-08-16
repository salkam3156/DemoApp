using DemoApp.PublicApi.Configuration.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DemoApp.PublicApi.Configuration.Loaders
{
    internal static class ControllersLoader
    {
        public static IServiceCollection AddControllersFromExternalAssembly(this IServiceCollection serviceCollection, Plugins options)
        {
             if (options is null) throw new ArgumentException(
                    $"Unable to load API controllers. Please provide configuration values in {nameof(Plugins)}");

            var controllersAssembly = GetConfiguredControllersAssembly(options);

            serviceCollection
                .AddControllers()
                .AddApplicationPart(controllersAssembly)
                .AddControllersAsServices();

            return serviceCollection;
        }

        private static Assembly GetConfiguredControllersAssembly(Plugins options)
        {
            var controllersAssemblyName = options.ControllersAssembly;

            return Assembly.Load(controllersAssemblyName);
        }
    }
}
