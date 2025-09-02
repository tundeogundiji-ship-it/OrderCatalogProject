using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Dormain;

namespace ProductCatalog.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("money");

            builder.Property(x => x.StockQuantity)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(150);

        }
    }
}
