using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ConfigurationSeed
{
    public class CartConfigurationSeed : IEntityTypeConfiguration<Data.Models.Cart>
    {
        public void Configure(EntityTypeBuilder<Data.Models.Cart> builder)
        {
            builder.HasData(
                new Data.Models.Cart()
                {
                    Id = 1,
                    Status = "Draft",
                    CreatedBy = "Aleksandar"
                }
                );
        }
    }
}
