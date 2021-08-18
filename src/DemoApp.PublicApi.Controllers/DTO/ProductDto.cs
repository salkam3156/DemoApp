using DemoApp.ApplicationCore.Entities;

namespace DemoApp.PublicApi.Controllers.DTO
{
    public record ProductDto
    {
        public string Name { get; init; }
        public ProductType Type { get; init; }
        public decimal Price { get; init; }
        public string Producer { get; init; }
        public string Description { get; init; }
    }
}
