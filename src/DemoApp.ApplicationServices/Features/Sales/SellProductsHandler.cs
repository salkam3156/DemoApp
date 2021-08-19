using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using DemoApp.ApplicationServices.Features.Products;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.ApplicationServices.Features.Sales
{
    public sealed class SellProductsHandler : IRequestHandler<SellProductsCommand, Result<Sale, DataAccessResult>>
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMediator _mediator;

        public SellProductsHandler(ISalesRepository salesRepository, IMediator mediator)
            => (_salesRepository, _mediator)= (salesRepository, mediator);

        public async Task<Result<Sale, DataAccessResult>> Handle(SellProductsCommand request, CancellationToken cancellationToken)
        {
            // TODO: implement unit of work for this or leave decoupled through messaging should the features and repos be split into separate assemblies
            var productsRequest = await _mediator.Send(new GetProductsByIdsRequest(request.ProductIds));

            var products = productsRequest.Extract(
                products => products,
                failure => /*log*/ Enumerable.Empty<Product>()
                );

            if (products.Any() is false) 
                return new Result<Sale, DataAccessResult>(DataAccessResult.UnableToCreate);

            var saleRecord = (await _salesRepository.CreateSaleRecordAsync(products))
                .Extract(
                    sale => sale,
                    failure => default
                );

            return saleRecord switch
            {
                Sale => new Result<Sale, DataAccessResult>(saleRecord),
                _ => new Result<Sale, DataAccessResult>(DataAccessResult.UnableToCreate)
            };
        }
    }
}
