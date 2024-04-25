namespace EatGoodNaija.Server.Model
{
    public class CustomerCart
    {
        public CustomerCart()
        {
        }

        public CustomerCart(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<CustomerCart> CustomerCarts { get; set; } = new List<CustomerCart>();
    }
}
