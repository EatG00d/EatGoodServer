namespace EatGoodNaija.Server.Model
{
    public class CustomerDish
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public string? PictureUrl { get; set; }
    }
}
