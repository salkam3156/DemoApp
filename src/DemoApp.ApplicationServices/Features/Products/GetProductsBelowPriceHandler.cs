using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using MediatR;

namespace DemoApp.ApplicationServices.Features.Products
{
    public class GetProductsBelowPriceHandler : IRequestHandler<GetProductsBelowPriceQuery, Result<IEnumerable<Product>, DataAccessResult>>
    {
        private readonly IProductsRepository _productsRepository;

        public GetProductsBelowPriceHandler(IProductsRepository productsRepository)
            => _productsRepository = productsRepository;
        
        public async Task<Result<IEnumerable<Product>, DataAccessResult>> Handle(GetProductsBelowPriceQuery request, CancellationToken cancellationToken)
        {
            return await _productsRepository.FindProductsBelowPriceAsync(request.Price);
        }
    }
}