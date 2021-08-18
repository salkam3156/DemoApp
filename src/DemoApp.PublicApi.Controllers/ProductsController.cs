using AutoMapper;
using DemoApp.ApplicationCore.Entities;
using DemoApp.PublicApi.Controllers.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApp.ApplicationServices.Features.Products;
using MediatR;

namespace DemoApp.PublicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator, IMapper mapper)
            => (_mediator, _mapper) = (mediator, mapper);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsBelowPrice([FromQuery] decimal price)
        {
            var queryResult = await _mediator.Send(new GetProductsBelowPriceQuery(price));
            
            var extractedResult = queryResult.Extract(
                products => products,
                fetchFailure => Enumerable.Empty<Product>());
            
            return extractedResult.Any() switch
            {
                true => Ok(_mapper.Map<List<ProductDto>>(extractedResult)),
                false => NotFound()
            };
        }
    }
}
