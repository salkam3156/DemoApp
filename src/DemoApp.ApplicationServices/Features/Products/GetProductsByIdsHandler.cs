﻿using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.ApplicationServices.Features.Products
{
    public sealed class GetProductsByIdsHandler : IRequestHandler<GetProductsByIdsRequest, Result<IEnumerable<Product>, DataAccessResult>>
    {
        private readonly IProductsRepository _productsRepository;

        public GetProductsByIdsHandler(IProductsRepository productsRepository)
            => _productsRepository = productsRepository;

        public async Task<Result<IEnumerable<Product>, DataAccessResult>> Handle(GetProductsByIdsRequest request, CancellationToken cancellationToken)
        {
            var allProductsQuery = await _productsRepository.FindProductsByIdsAsync(request.ProductIds);

            var allProducts = allProductsQuery.Extract(
                products => products,
                failure => /*log*/ Enumerable.Empty<Product>()
                );

            return (allProducts.Count() == request.ProductIds.Length) switch
            {
                true => new Result<IEnumerable<Product>, DataAccessResult>(allProducts),
                false => new Result<IEnumerable<Product>, DataAccessResult>(DataAccessResult.NotAllFound)
            };
        }
    }
}
