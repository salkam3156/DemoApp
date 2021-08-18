using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using MediatR;

namespace DemoApp.ApplicationServices.Features.Sales
{
    public sealed record SellProductsCommand : IRequest<Result<Sale, DataAccessResult>>
    {
        public int[] ProductIds { get; private init; }

        public SellProductsCommand(int[] productIds)
            => ProductIds = productIds;

    }
}
