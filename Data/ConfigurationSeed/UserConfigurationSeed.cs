using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ConfigurationSeed
{
    public class UserConfigurationSeed : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();

            var user1 = new IdentityUser
            {
                Id = "1",
                UserName = "Aleksandar",
                NormalizedUserName = "ALEKSANDAR"
            };

            var user2 = new IdentityUser
            {
                Id = "2",
                UserName = "Milos",
                NormalizedUserName = "MILOS"
            };
            
            user1.PasswordHash = ph.HashPassword(user1, "testpassword1");

            user2.PasswordHash = ph.HashPassword(user2, "testpassword2");

            builder.HasData(user1,user2);
        }
    }
}
