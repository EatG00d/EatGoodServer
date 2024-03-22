using System.ComponentModel.DataAnnotations;

namespace EatGoodNaija.Server.Model.DTO.authenticationDTO
{
    public class resetPasswordDTO
    {
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
