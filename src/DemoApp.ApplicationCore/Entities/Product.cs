using DemoApp.ApplicationCore.GeneralAbstractions;
using System;

namespace DemoApp.ApplicationCore.Entities
{
    public sealed record Product 
    {
        public int Id { get; private init; }
        public string Name { get; private init; }
        public ProductType Type { get; private init; }
        public decimal Price { get; private init; }
        public string Manufacturer { get; private init; }
        public string Description { get; private init; }

        private Product() { }
        public Product(int id, string name, ProductType type, decimal price, string manufacturer, string description = "")
        {
            Validation.ThrowIfAnyAreFalse(
                () => id >= 1,
                () => string.IsNullOrWhiteSpace(name) is false,
                () => string.IsNullOrWhiteSpace(manufacturer) is false,
                () => Enum.IsDefined(type));
            
            Id = id;
            Name = name;
            Type = type;
            Price = price < 0m ? 0m : price;
            Manufacturer = manufacturer;
            Description = description;
        }
    }

    [Flags]
    public enum ProductType 
    { 
        Peripheral = 1 << 0,
        Display = 1 << 1,
        GraphicsCard = 1 << 2,
        Motherboard = 1 << 3,
        Mouse = 1 << 4,
        Keyboard = 1 << 5,
        Miscellaneous = 1 << 6,
        GamingConsole = 1 << 7,
        Software = 1 << 8,
    }
}


