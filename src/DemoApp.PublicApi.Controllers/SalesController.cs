using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationServices.Features.Sales;
using DemoApp.PublicApi.Controllers.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp.PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{saleId}", Name = nameof(GetSaleRecordById))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaleResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SaleResponseDto>> GetSaleRecordById([FromRoute] int saleId)
        {
            var saleQueryResult = await _mediator.Send(new GetSaleRecordByIdRequest(saleId));

            var extractedSale = saleQueryResult.Extract(
                sale => sale,
                failure => default
                ) ;

            return extractedSale switch
            {
                Sale => Ok(extractedSale),
                _    => NotFound()
            };
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaleResponseDto))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<SaleResponseDto>> SellProducts([FromBody] List<int> productsToSell)
        {
            var saleCommandResult = await _mediator.Send(new SellProductsCommand(productsToSell.ToArray()));

            var extractedSale = saleCommandResult.Extract(
                sale => sale,
                // TODO: change faulted result type for sales and add logging
                failure => { /*log*/ return default; }
                );

            var namo = nameof(SalesController.GetSaleRecordById);

            return extractedSale switch
            {
                Sale => CreatedAtRoute(nameof(SalesController.GetSaleRecordById), new { saleId = extractedSale.Id }, extractedSale),
                _ => UnprocessableEntity() /* some validation or processing failed, logged before in extraction [TODO]
                                            *, else we wouldn't be here because of an infrastructure error */
            };
        }
    }
}
