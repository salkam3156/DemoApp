using AutoMapper;
using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.RepositoryContracts;
using DemoApp.PublicApi.Controllers.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.PublicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductsRepository productsRepository, IMapper mapper)
            => (_productsRepository, _mapper) = (productsRepository, mapper);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsBelowPrice([FromQuery] decimal price)
        {
            var productsBelowPrice = await _productsRepository.FindProductsBelowPriceAsync(price);

            var extractedResult = productsBelowPrice.Extract(
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
