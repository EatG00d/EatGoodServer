using System.ComponentModel.DataAnnotations;

namespace EatGoodNaija.Server.Model.DTO.profileDTO
{
    public class UpdateProfileDTO
    {
        
        [Required(ErrorMessage ="PhoneNumber is Required")]
        [Phone(ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="HomeAddress is Required")]
        public string HomeAddress { get; set; }
        [Required(ErrorMessage ="City is Required")]
        public string City { get; set; }
    }
}
