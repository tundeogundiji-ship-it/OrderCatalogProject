using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Dormain;


namespace ProductCatalog.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.UserId)
             .IsRequired();

            builder.Property(x => x.TotalAmount)
                .IsRequired();

            builder.Property(x => x.OrderDate)
               .IsRequired();

        }
    }
}
