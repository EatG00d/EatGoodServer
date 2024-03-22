namespace EatGoodNaija.Server.Model
{
    public class Order
    {
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
