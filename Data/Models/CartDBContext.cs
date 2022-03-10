using Data.ConfigurationSeed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Data.Models
{
    public partial class CartDBContext : IdentityDbContext<IdentityUser>
    {
       

        public CartDBContext(DbContextOptions<CartDBContext> options) :base(options)
        {

        }


        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
       

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.TimeCreated).HasComputedColumnSql("(sysdatetimeoffset())", false);

                entity.Property(e => e.TimeUpdated).HasDefaultValueSql("(sysdatetimeoffset())");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.Property(e => e.TimeCreated).HasComputedColumnSql("(sysdatetimeoffset())", false);

                entity.Property(e => e.TimeUpdated).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK_CartItem_Cart");
            });

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfigurationSeed());

            modelBuilder.ApplyConfiguration(new UserConfigurationSeed());

            modelBuilder.ApplyConfiguration(new UserRoleConfigurationSeed());

            modelBuilder.ApplyConfiguration(new CartConfigurationSeed());

            modelBuilder.ApplyConfiguration(new CartItemConfigurationSeed());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
