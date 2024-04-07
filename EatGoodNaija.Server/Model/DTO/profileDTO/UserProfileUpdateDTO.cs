using Org.BouncyCastle.Asn1.Crmf;
using System.ComponentModel.DataAnnotations;

namespace EatGoodNaija.Server.Model.DTO.profileDTO
{
    public class UserProfileUpdateDTO
    {
        public string FullName { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Phone Number is Required")]
        [Phone(ErrorMessage ="Invalid Phone Number")]
         public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="Home Address is Required")]
        public string HomeAddress { get; set; }
        [Required]
        public string City { get; set; }


    }
}
