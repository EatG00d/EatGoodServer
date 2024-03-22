namespace EatGoodNaija.Server.Model.DTO.authenticationDTO
{
    public class ConfirmEmailToken
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Token { get; set; }
        public string VendorId { get; set; }
        public Vendor vendor { get; set; }
    }
}
