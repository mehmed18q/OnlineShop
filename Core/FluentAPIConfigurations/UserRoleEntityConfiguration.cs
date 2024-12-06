using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.FluentAPIConfigurations
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            _ = builder.ToTable("UserRoles", "Base");

            _ = builder.HasKey(userRole => userRole.Id);

            _ = builder.HasOne(userRole => userRole.User);
            _ = builder.HasOne(userRole => userRole.Role);
        }
    }
}
