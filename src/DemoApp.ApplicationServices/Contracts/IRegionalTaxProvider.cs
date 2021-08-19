using DemoApp.ApplicationCore.Models;
using DemoApp.ApplicationServices.Models;
using System.Threading.Tasks;

namespace DemoApp.ApplicationServices.Contracts
{
    public interface IRegionalTaxProvider
    {
        Task<Tax> GetTaxForRegionAsync(TaxRegion region);
    }
}
