using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using MediatR;
using System.Collections.Generic;

namespace DemoApp.ApplicationServices.Features.Products
{
    public sealed class GetProductsByIdsRequest : IRequest<Result<IEnumerable<Product>, DataAccessResult>>
    {
        public int[] ProductIds { get; private init; }
        public GetProductsByIdsRequest(int[] productIds)
            => ProductIds = productIds;
    }
}
