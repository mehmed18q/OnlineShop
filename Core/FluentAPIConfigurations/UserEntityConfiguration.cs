using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.FluentAPIConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            _ = builder.ToTable("Users", "Base");

            _ = builder.HasKey(user => user.Id);

            _ = builder.HasMany(user => user.UserRoles);

            _ = builder.Property(user => user.UserName)
                .HasMaxLength(64);

            _ = builder.Property(user => user.Password)
               .HasMaxLength(200);

            _ = builder.Property(user => user.PasswordSalt)
            .HasMaxLength(200);
        }
    }
}
