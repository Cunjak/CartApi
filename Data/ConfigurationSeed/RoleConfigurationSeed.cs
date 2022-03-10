using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ConfigurationSeed
{
    public class RoleConfigurationSeed : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable("AspNetRoles");

            builder.HasData(
                new IdentityRole { Id = "Standard", Name = "Standard" },
                new IdentityRole { Id = "Viewer", Name = "Viewer" }
                );      
        }
    }
}
