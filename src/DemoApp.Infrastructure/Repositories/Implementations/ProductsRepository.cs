using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
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
        public ProductsRepository(DomainContext ctx) : base(ctx) {}
        public DomainContext AppContext
            => Ctx as DomainContext
            ?? throw new DatabaseAccessException($"{nameof(AppContext)} could not be accessed due to failed DB dependency.");

        public Task<Result<IEnumerable<Product>, DataAccessResult>> FindProductsBelowPriceAsync(decimal price)
        {
            var productsBelowPrice = AppContext
                .Products
                .Where(product => product.Price < price);
            
            return Task.FromResult(
                new Result<IEnumerable<Product>, DataAccessResult>(productsBelowPrice)
                );
        }

        public async Task<Result<IEnumerable<Product>, DataAccessResult>> FindProductsByIdsAsync(IEnumerable<int> ids)
        {
            // TODO: change in the future to "not dumb" version / thread pool starvation indeterminate number of method calls etc . Should be a match / join
            var matchedProductsTasks = ids.Select(async id => await AppContext.Products.FindAsync(id));
            var mathedProducts = (await Task.WhenAll(matchedProductsTasks)).Where(product => product is not null).ToList();

            //TODO: this instantiates 2 enumerators, so think about that
            return (mathedProducts.Count() == ids.Count()) switch
            {
                true => new Result<IEnumerable<Product>, DataAccessResult>(mathedProducts),
                // TODO: change the DataAccessResult to a more robuste type, template it and provide difference a la metched.Except(expected) or a text report
                false => new Result<IEnumerable<Product>, DataAccessResult>(DataAccessResult.NotAllFound)
            };
        }
    }
}
