using Basket.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Basket.API.Data
{
    public class BasketDbContext : DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> opt) : base(opt)
        {

        }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShoppingCart>()
               .HasIndex(s => s.UserName)
            .IsUnique();

            builder.Entity<ShoppingCart>()
            .HasMany(s => s.Items)
            .WithOne(si => si.ShoppingCart)
            .HasForeignKey(s => s.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ShoppingCartItem>()
               .HasOne(si => si.ShoppingCart)
               .WithMany(s => s.Items)
               .HasForeignKey(si => si.ShoppingCartId);
        }
    }
}
