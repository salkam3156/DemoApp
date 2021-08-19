using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using System.Threading.Tasks;
using System.Collections.Generic;
using DemoApp.ApplicationCore.GeneralAbstractions;

namespace DemoApp.ApplicationCore.RepositoryContracts
{
    public interface IProductsRepository
    {
        public Task<Result<IEnumerable<Product>, DataAccessResult>> FindProductsBelowPriceAsync(decimal price);
        public Task<Result<IEnumerable<Product>, DataAccessResult>> FindProductsByIdsAsync(IEnumerable<int> ids);
    }
}
