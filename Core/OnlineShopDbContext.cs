using Core.Entities;
using Core.FluentAPIConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class OnlineShopDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            _ = modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}
