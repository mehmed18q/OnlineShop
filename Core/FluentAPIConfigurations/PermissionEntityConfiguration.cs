using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.FluentAPIConfigurations
{
    public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            _ = builder.ToTable("Permissions", "Base");

            _ = builder.HasKey(permission => permission.Id);
            _ = builder.HasMany(permission => permission.RolePermissions);

            _ = builder.Property(permission => permission.Title)
                          .HasMaxLength(256);

            _ = builder.Property(permission => permission.Flag)
                          .HasMaxLength(256);
        }
    }
}
