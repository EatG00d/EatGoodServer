using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Model.DTO.authenticationDTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EatGoodNaija.Server.Data
{
    public class DataContext : IdentityDbContext<Vendor>
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        

        public DbSet<Vendor> Vendor { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<ConfirmEmailToken> ConfirmEmailTokens { get; set; }


    }
}
