﻿using DemoApp.ApplicationCore.Entities;
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
    }
}
