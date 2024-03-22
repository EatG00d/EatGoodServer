using System.ComponentModel.DataAnnotations;

namespace EatGoodNaija.Server.Model.DTO.authenticationDTO
{
    public class registerDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}
