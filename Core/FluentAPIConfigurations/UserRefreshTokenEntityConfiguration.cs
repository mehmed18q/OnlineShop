using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.FluentAPIConfigurations
{
    public class UserRefreshTokenEntityConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            _ = builder.ToTable("UserRefreshTokens", "Base");

            _ = builder.HasKey(userRefreshToken => userRefreshToken.Id);

            _ = builder.HasOne(userRefreshToken => userRefreshToken.User);

            _ = builder.Property(userRefreshToken => userRefreshToken.RefreshToken)
                .HasMaxLength(128);
        }
    }
}
