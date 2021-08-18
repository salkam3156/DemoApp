using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.RepositoryContracts;
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
        public ProductsController(IProductsRepository productsRepository)
            => _productsRepository = productsRepository;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsBelowPrice([FromQuery] decimal price)
        {
            var productsBelowPrice = await _productsRepository.FindProductsBelowPriceAsync(price);

            var extractedResult = productsBelowPrice.Extract(
                products => products,
                fetchFailure => Enumerable.Empty<Product>());
            
            return Ok(extractedResult);
        }
    }
}
