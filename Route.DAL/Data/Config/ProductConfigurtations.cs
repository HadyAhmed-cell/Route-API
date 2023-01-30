using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.DAL.Entities;

namespace Route.DAL.Data.Config
{
    internal class ProductConfigurtations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Price).HasColumnType("decimal(10,2)");
            builder.Property(x => x.PictureUrl).IsRequired();

        }
    }
}
