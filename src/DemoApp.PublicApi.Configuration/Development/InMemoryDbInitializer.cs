using DemoApp.ApplicationCore.Entities;
using DemoApp.Infrastructure.Repositories.DbContexts;
using System;
using System.Threading.Tasks;

namespace DemoApp.PublicApi.Configuration.Development
{
    internal static class InMemoryDbInitializer
    {
        public static async Task Initialize(DomainContext context)
        {
            var products = new[]
            {
                new Product(id: 1, name: "Razor Mechanical Keyboard GX20-652/I.BNC IXI eXtriimzzzz", manufacturer: "Razor", price: 1000.99m, type: ProductType.Keyboard, description: "Buying this may be the \"key\" to upping your typing experience."),
                new Product(id: 2, name: "Samsung HD IPS 1M 224 / PH 144826-ikbr 24\"", manufacturer: "Samsung", price: 250.99m, type: ProductType.Display, description: "IPS display. 1 000 000 Hz refresh rate. Image prediction - can play out the entire game based on the first 2 frames, as long as it's a Taito SHMUP. Now everyone can focus on the image quality as opposed to playing, for an affordable price."),
                new Product(id: 3, name: "Playstation 5", manufacturer: "Sony", price: 299.99m, type: ProductType.GamingConsole, description: "Not a PC."),
                new Product(id: 4, name: "Super Puzzle Fighter 2 Turbo HD Remix", manufacturer: "Capcom", price: 29.99m, type: ProductType.Software, description: "Classic puzzler."),
            };

            await context.Products.AddRangeAsync(
                products
            );

            await context.Sales.AddRangeAsync(new[]
            {
                new Sale(1, 23.0m, DateTime.Now, products[0..1])
            });

            if (await context.SaveChangesAsync() == 0) throw new Exception("Failed to initialize the development database.");
        }
    }
}
