using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using System.Threading.Tasks;

namespace DemoApp.ApplicationCore.RepositoryContracts
{
    public interface ISalesRepository
    {
        public Task<Result<Sale, DataAccessResult>> CreateSaleRecordAsync(int[] ids);
    }
}

