using DemoApp.ApplicationCore.GeneralAbstractions;
using System;

namespace DemoApp.ApplicationCore.Entities
{
    public sealed record Product 
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ProductType Type { get; init; }
        public decimal Price { get; init; }
        public string Producer { get; init; }
        public string Description { get; init; }

        private Product() { }
        public Product(int id, string name, ProductType type, decimal price, string manufacturer, string description = "")
        {
            Validation.ThrowIfAnyAreFalse(
                () => id >= 1,
                () => string.IsNullOrWhiteSpace(name) is false,
                () => string.IsNullOrWhiteSpace(manufacturer) is false,
                () => Enum.IsDefined<ProductType>(type),
                () => price > 0);
            
            Id = id;
            Name = name;
            Type = type;
            Price = price;
            Producer = manufacturer;
            Description = description;
        }
    }

    [Flags]
    public enum ProductType 
    { 
        Peripheral = 1 << 0,
        Display = 1 << 1,
        GraphicsCard = 1 << 2,
        MotherBoard = 1 << 3,
        Mouse = 1 << 4,
        Keyboard = 1 << 5,
        Micallaneous = 1 << 6,
        GamingConsole = 1 << 7,
        Software = 1 << 8,
    }
}


