using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using DemoApp.ApplicationServices.Contracts;
using DemoApp.ApplicationServices.Features.Products;
using DemoApp.ApplicationServices.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.ApplicationServices.Features.Sales
{
    public sealed class SellProductsHandler : IRequestHandler<SellProductsCommand, Result<Sale, DataAccessResult>>
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMediator _mediator;
        private readonly IRegionalTaxProvider _regionalTaxProvider;
        private readonly ILogger<SellProductsHandler> _logger;

        public SellProductsHandler(ISalesRepository salesRepository, IRegionalTaxProvider regionalTaxProvider, IMediator mediator, ILogger<SellProductsHandler> logger)
            =>  (_salesRepository, _regionalTaxProvider, _mediator, _logger) 
                = (salesRepository, regionalTaxProvider, mediator, logger);

        public async Task<Result<Sale, DataAccessResult>> Handle(SellProductsCommand request, CancellationToken cancellationToken)
        {
            // TODO: implement unit of work for this or leave decoupled through messaging should the features and repos be split into separate assemblies
            var productsRequest = await _mediator.Send(new GetProductsByIdsRequest(request.ProductIds));

            var products = productsRequest.Extract(
                products => products,
                failure => 
                {
                    _logger.LogError($"{nameof(SellProductsHandler)}: Failed fetching products to sell details. Reason: {failure}");
                    return Enumerable.Empty<Product>();
                });

            var applicableTax = await _regionalTaxProvider
                .GetTaxForRegionAsync(new TaxRegion("USA" /* or this_region_configured_or_retrieved_from_somewhere.
                                                      * APIs should provide stronger typing though nugets or swagger documentation but it's an exmaple,
                                                      * plus this depends on the API design*/));

            var saleRecordCreationRequest = await _salesRepository.CreateSaleRecordAsync(products, applicableTax);
                
            var saleRecord = saleRecordCreationRequest.Extract(
                    sale => sale,
                    failure => { _logger.LogError($"{nameof(SellProductsHandler)}: Unable to create sale record. Reason: {failure}"); return default; }
                );

            return saleRecord switch
            {
                Sale => new Result<Sale, DataAccessResult>(saleRecord),
                _ => new Result<Sale, DataAccessResult>(DataAccessResult.UnableToCreate)
            };
        }
    }
}
