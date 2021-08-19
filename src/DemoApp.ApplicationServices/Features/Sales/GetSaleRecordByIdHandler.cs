using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.ApplicationServices.Features.Sales
{
    public class GetSaleRecordByIdHandler : IRequestHandler<GetSaleRecordByIdRequest, Result<Sale, DataAccessResult>>
    {
        private readonly ISalesRepository _salesRepository;

        public GetSaleRecordByIdHandler(ISalesRepository salesRepository)
            => _salesRepository = salesRepository;
            
        public async Task<Result<Sale, DataAccessResult>> Handle(GetSaleRecordByIdRequest request, CancellationToken cancellationToken)
        {
            return await _salesRepository.FindSaleRecordAsync(request.SaleId);
        }
    }
}
