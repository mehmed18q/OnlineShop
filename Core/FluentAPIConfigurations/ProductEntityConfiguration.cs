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

            _ = builder.HasKey(product => product.Id);

            _ = builder.Property(product => product.ProductName)
                .HasColumnName("Title")
                .HasMaxLength(256)
                .HasColumnOrder(1);

            _ = builder.Property(product => product.Description)
                .HasMaxLength(1000);
        }
    }
}
