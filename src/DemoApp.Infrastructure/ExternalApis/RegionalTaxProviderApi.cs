using DemoApp.ApplicationCore.Models;
using DemoApp.ApplicationServices.Contracts;
using DemoApp.ApplicationServices.Models;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.ExternalApis
{
    public sealed class RegionalTaxProviderApi : IRegionalTaxProvider
    {
        // some external API would do this / this would be an upstream, dependency call
        // doesn't conform to the application conventions so would need a wrapper / adapter normally to keep application conventions intact

        private static class KnownRegions
        {
            public static decimal USA => 15m;
        }

        public Task<Tax> GetTaxForRegionAsync(TaxRegion region)
        {
            return region.Region switch
            {
                nameof(KnownRegions.USA) => Task.FromResult(new Tax(KnownRegions.USA)),
                _ => throw new FailedPublicDependencyException($"{nameof(RegionalTaxProviderApi)} does not know this tax region") // won't be caught by error controller
                                                                                                                                  // we expect this service to catch it and return some sensible result,
                                                                                                                                  //but it's up to them
            };
        }
    }
}
