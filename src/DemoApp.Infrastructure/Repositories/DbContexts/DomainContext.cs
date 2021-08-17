using DemoApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Infrastructure.Repositories.DbContexts
{
    public sealed class DomainContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public DomainContext(DbContextOptions<DomainContext> contextOptions) : base(contextOptions) { }
    }
}
