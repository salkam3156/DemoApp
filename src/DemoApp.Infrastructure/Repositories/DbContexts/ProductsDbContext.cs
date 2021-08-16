using DemoApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Infrastructure.Repositories.DbContexts
{
    public sealed class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public ProductsDbContext(DbContextOptions<ProductsDbContext> contextOptions) : base(contextOptions) { }
    }
}
