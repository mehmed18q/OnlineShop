using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.FluentAPIConfigurations
{

    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            _ = builder.ToTable("Roles", "Base");

            _ = builder.HasKey(role => role.Id);
            _ = builder.HasMany(role => role.UserRoles);
            _ = builder.HasMany(role => role.RolePermissions);

            _ = builder.Property(role => role.Title)
                       .HasMaxLength(256);
        }
    }
}
