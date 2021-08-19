using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.ApplicationCore.RepositoryContracts
{
    public interface ISalesRepository
    {
        public Task<Result<Sale, DataAccessResult>> CreateSaleRecordAsync(IEnumerable<Product> products);
        public Task<Result<Sale, DataAccessResult>> FindSaleRecord(int id);
    }
}

