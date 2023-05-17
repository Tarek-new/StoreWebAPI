using Core.Enitities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShippedToAddress);
            builder.Property(P => P.OrderStatus)
                .HasConversion(
                    s => s.ToString(),
                    value => (OrderStatus)Enum.Parse(typeof(OrderStatus), value));
            builder.HasMany(order => order.OrderItems)
                .WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
