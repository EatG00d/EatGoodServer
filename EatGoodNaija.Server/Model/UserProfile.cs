using System.ComponentModel.DataAnnotations;

namespace EatGoodNaija.Server.Model
{
    public class UserProfile
    {
        [Key]
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeAddress { get; set; }
        public string City { get; set; }
    }
}
