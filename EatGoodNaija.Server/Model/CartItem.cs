namespace EatGoodNaija.Server.Model
{
    public class CartItem
    {
        public string Id { get; set; }
        public string FoodItemId { get; set; }
        public string Quantity { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
