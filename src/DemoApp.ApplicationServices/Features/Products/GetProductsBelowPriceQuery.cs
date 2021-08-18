using System.Collections.Generic;
using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using MediatR;

namespace DemoApp.ApplicationServices.Features.Products
{
    public sealed record GetProductsBelowPriceQuery : IRequest<Result<IEnumerable<Product>, DataAccessResult>>
    {
        public decimal Price { get; init; }

        public GetProductsBelowPriceQuery(decimal price)
            => Price = price;
    }
}