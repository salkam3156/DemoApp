using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp.ApplicationCore.RepositoryContracts
{
    public interface ISalesRepository
    {
        public Task<Result<Sale, DataAccessResult>> CreateSaleRecordAsync(IEnumerable<Product> products, Tax taxRate);
        public Task<Result<Sale, DataAccessResult>> FindSaleRecordAsync(int id);
    }
}

