namespace EatGoodNaija.Server.Model
{
    public class Customer
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
