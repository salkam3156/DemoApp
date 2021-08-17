using DemoApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Infrastructure.Repositories.DbContexts
{
    public sealed class AppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppContext(DbContextOptions<AppContext> contextOptions) : base(contextOptions) { }
    }
}
