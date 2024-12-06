using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.FluentAPIConfigurations
{
    public class RolePermissionEntityConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            _ = builder.ToTable("RolePermissions", "Base");
            _ = builder.HasKey(rolePermission => rolePermission.Id);

            _ = builder.HasOne(rolePermission => rolePermission.Permission);
            _ = builder.HasOne(rolePermission => rolePermission.Role);

        }
    }
}
