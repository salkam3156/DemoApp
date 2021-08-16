
namespace DemoApp.ApplicationCore.Entities
{
    public sealed record Product
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public float Price { get; init; }
        public string Description { get; init; }

    }
}
