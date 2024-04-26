using EatGoodNaija.Server.Model.DTO;
using EatGoodNaija.Server.Model.DTO.authenticationDTO;
using EatGoodNaija.Server.Model.DTO.profileDTO;

namespace EatGoodNaija.Server.Services.Interface
{
    public interface IAuthService
    {
        Task<ResponseDTO<string>> RegisterAsync(registerDTO registerDto, List<string> roles);
        Task<ResponseDTO<string>> LoginAsync(loginDTO loginDto);

        Task<ResponseDTO<string>> ForgotPasswordAsync(string userEmail);
        Task<ResponseDTO<string>> ResetPasswordAsync(resetPasswordDTO newPassword);
        Task<ResponseDTO<string>> ConfirmEmailAsync(int token, string email);
        Task<bool> UpdateProfileAsync(UpdateProfileDTO updateProfileDTO, string Id);
        //Task<Re

    }
}
