using Core.Entities;
using Core.Entities.Security;
using Core.FluentAPIConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class OnlineShopDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Permission> Permission => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            _ = modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            _ = modelBuilder.ApplyConfiguration(new UserRefreshTokenEntityConfiguration());
            _ = modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            _ = modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
            _ = modelBuilder.ApplyConfiguration(new PermissionEntityConfiguration());
            _ = modelBuilder.ApplyConfiguration(new RolePermissionEntityConfiguration());
        }
    }
}
