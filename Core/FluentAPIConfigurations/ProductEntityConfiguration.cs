using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.FluentAPIConfigurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            _ = builder.ToTable("Products", "Base");

            _ = builder.HasKey(p => p.Id);

            _ = builder.Property(p => p.ProductName)
                .HasColumnName("Title")
                .HasMaxLength(256)
                .HasColumnOrder(1);
        }
    }
}
