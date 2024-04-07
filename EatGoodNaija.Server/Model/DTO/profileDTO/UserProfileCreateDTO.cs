using System.ComponentModel.DataAnnotations;

namespace EatGoodNaija.Server.Model.DTO.profileDTO
{
    public class UserProfileCreateDTO
    {
        public string FullName { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage ="PhoneNumber is Required")]
        [Phone(ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="HomeAddress is Required")]
        public string HomeAddress { get; set; }
        [Required(ErrorMessage ="City is Required")]
        public string City { get; set; }
    }
}
