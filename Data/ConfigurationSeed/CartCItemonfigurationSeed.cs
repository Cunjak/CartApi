using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ConfigurationSeed
{
    public class CartItemConfigurationSeed : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasData(
                new CartItem()
                {
                    Id = 1,
                    CartId = 1,
                    Name = "Laptop",
                    Description = "Dell Vostro 3500 256 GB SSD",
                    CreatedBy = "Aleksandar"
                },
                new CartItem()
                {
                    Id = 2,
                    CartId = 1,
                    Name = "Phone",
                    Description = "Xiaomi Redmi 6A",
                    CreatedBy = "Aleksandar"
                },
                new CartItem()
                {
                    Id = 3,
                    CartId = 1,
                    Name = "Phone",
                    Description = "Xiaomi Mi 10",
                    CreatedBy = "Aleksandar"
                }
                );
        }
    }
}
