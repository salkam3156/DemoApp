using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.ApplicationServices.Features.Sales
{
    public class GetSaleRecordByIdHandler : IRequestHandler<GetSaleRecordByIdRequest, Result<Sale, DataAccessResult>>
    {
        public Task<Result<Sale, DataAccessResult>> Handle(GetSaleRecordByIdRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
