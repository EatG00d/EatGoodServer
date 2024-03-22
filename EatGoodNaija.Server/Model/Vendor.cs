using Microsoft.AspNetCore.Identity;

namespace EatGoodNaija.Server.Model
{
    public class Vendor : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
