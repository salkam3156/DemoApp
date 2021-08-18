using DemoApp.PublicApi.Configuration.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using MediatR;

namespace DemoApp.PublicApi.Configuration.Loaders
{
    internal static class ApplicationFeaturesLoader
    {
        //TODO: should be a template method with other loader
        
        public static IServiceCollection AddApplicationFeatures(this IServiceCollection serviceCollection, Plugins options)
        {
            // TODO: argumentExceptionCreator taking in (originatingMethod, reason)
            if (options is null) throw new ArgumentException(
                $"Unable to execute {MethodBase.GetCurrentMethod().Name}  due to configutation issue. Please provide configuration values in {nameof(Plugins)}");

            var featuresAssembly = GetConfiguredFeaturesAssembly(options);
            
            serviceCollection.RegisterFeaturesFromAssembly(featuresAssembly);

            return serviceCollection;
        }

        private static IServiceCollection RegisterFeaturesFromAssembly(this IServiceCollection serviceCollection, Assembly featuresAssembly)
        {
            serviceCollection.AddMediatR(featuresAssembly);

            return serviceCollection;
        }

        private static Assembly GetConfiguredFeaturesAssembly(Plugins options)
        {
            var featuresAssemblyName = options.FeaturesAssembly;

            return Assembly.Load(featuresAssemblyName);
        }
    }
}