using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.DAL.Entities.Order;

namespace Route.DAL.Data.Config
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShipToAddress, np =>
            {
                np.WithOwner();
            });
            builder.Property(o => o.Status)
                .HasConversion(
                OrderStatus => OrderStatus.ToString(),
                OrderStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OrderStatus)

                );
            builder.HasMany(o => o.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
