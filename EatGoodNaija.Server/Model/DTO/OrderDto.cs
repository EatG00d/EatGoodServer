namespace EatGoodNaija.Server.Model.DTO
{
    public class OrderDto
    {
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
