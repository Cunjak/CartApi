using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ConfigurationSeed
{
    public class UserRoleConfigurationSeed : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string> { RoleId = "Standard", UserId = "1"},
                new IdentityUserRole<string> { RoleId = "Viewer", UserId = "2" }
                );
        }
    }
}
