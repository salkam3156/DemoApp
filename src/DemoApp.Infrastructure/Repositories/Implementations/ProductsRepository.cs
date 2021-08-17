
using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enumerations;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using DemoApp.Infrastructure.Exceptions.Database;
using DemoApp.Infrastructure.Repositories.DbContexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.Repositories.Implementations
{
    public sealed class ProductsRepository : AsyncRepositoryBase<Product>, IProductsRepository
    {
        public ProductsRepository(AppContext ctx) : base(ctx) {}
        public AppContext AppContext
            => Ctx as AppContext 
            ?? throw new DatabaseAccessException($"{nameof(AppContext)} could not be accessed due to failed DB dependency.");

        public Task<Option<IEnumerable<Product>, RepositoryFailure>> FindProductsBelowPrice(float price)
        {
            var productsBelowPrice = AppContext
                .Products
                .Where(product => product.Price < price);
            
            return Task.FromResult(
                new Option<IEnumerable<Product>, RepositoryFailure>(productsBelowPrice)
                );
        }
    }
}
