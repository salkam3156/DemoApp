using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using MediatR;

namespace DemoApp.ApplicationServices.Features.Sales
{
    public sealed record GetSaleRecordByIdRequest : IRequest<Result<Sale, DataAccessResult>>
    {
        public int SaleId { get; private init; }

        public GetSaleRecordByIdRequest(int saleId)
            => SaleId = saleId;
    }
}
