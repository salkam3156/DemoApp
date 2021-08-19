using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using DemoApp.Infrastructure.Exceptions.Database;
using DemoApp.Infrastructure.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.Repositories.Implementations
{
    public sealed class SalesRepository : AsyncRepositoryBase<Product>, ISalesRepository
    {
        public SalesRepository(DomainContext ctx) : base(ctx) {}
        public DomainContext AppContext
            => Ctx as DomainContext
            ?? throw new DatabaseAccessException($"{nameof(AppContext)} could not be accessed due to failed DB dependency.");

        public async Task<Result<Sale, DataAccessResult>> CreateSaleRecordAsync(IEnumerable<Product> products)
        {
            var saleRecordCreationResult = await AppContext.Sales.AddAsync(new Sale(23.0m /* some service would be supplying tax details via param */, products));

            _ = await AppContext.SaveChangesAsync();

            return saleRecordCreationResult.Entity switch
            {
                Sale => new Result<Sale, DataAccessResult>(saleRecordCreationResult.Entity),
                _    => new Result<Sale, DataAccessResult>(DataAccessResult.UnableToCreate)
            };
        }

        public async Task<Result<Sale, DataAccessResult>> FindSaleRecord(int id)
        {
            var saleRecord = await AppContext.Sales.FindAsync(id);

            return saleRecord switch
            {
                Sale => new Result<Sale, DataAccessResult>(saleRecord),
                _ => new Result<Sale, DataAccessResult>(DataAccessResult.NotFound)
            };
        }
    }
}
