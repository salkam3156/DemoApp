using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.ApplicationServices.Features.Products
{
    public sealed class GetProductsHandler : IRequestHandler<GetProductsQuery, Result<IEnumerable<Product>, DataAccessResult>>
    {
        private readonly IProductsRepository _productsRepository;

        public GetProductsHandler(IProductsRepository productsRepository)
            => _productsRepository = productsRepository;
        
        public async Task<Result<IEnumerable<Product>, DataAccessResult>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var repositoryCrudBase = _productsRepository as IAsyncReposiroty<Product>;

            var allProducts = await repositoryCrudBase.GetAllAsync();

            return allProducts;
        }
    }
}
