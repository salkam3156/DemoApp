using DemoApp.ApplicationCore.Entities;

namespace DemoApp.PublicApi.Controllers.DTO
{
    public record ProductResponseDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ProductType Type { get; init; }
        public decimal Price { get; init; }
        public string Manufacturer { get; init; }
        public string Description { get; init; }
    }
}
