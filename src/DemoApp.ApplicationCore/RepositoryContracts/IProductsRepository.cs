using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enumerations;
using System.Threading.Tasks;
using System.Collections.Generic;
using DemoApp.ApplicationCore.GeneralAbstractions;

namespace DemoApp.ApplicationCore.RepositoryContracts
{
    public interface IProductsRepository
    {
        public Task<Option<IEnumerable<Product>, RepositoryFailure>> FindProductsBelowPrice(float price);
    }
}
