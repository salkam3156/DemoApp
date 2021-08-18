using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.ApplicationServices.Features.Sales
{
    public sealed class SellProductsHandler : IRequestHandler<SellProductsCommand, Result<Sale, DataAccessResult>>
    {
        private readonly ISalesRepository _salesRepository;

        public SellProductsHandler(ISalesRepository salesRepository)
            => _salesRepository = salesRepository;

        public Task<Result<Sale, DataAccessResult>> Handle(SellProductsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
