using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Mozilla;

namespace EatGoodNaija.Server.Model
{
    public class Vendor : IdentityUser
    {
        //public string userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? HomeAddress { get; set; }
        public string? city { get; set; }
        public List<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
