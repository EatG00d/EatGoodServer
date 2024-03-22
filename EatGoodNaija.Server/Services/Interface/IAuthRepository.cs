using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Model.DTO.authenticationDTO;

namespace EatGoodNaija.Server.Services.Interface
{
    public interface IAuthRepository
    {
        Task<Vendor> SignUpAsync(Vendor vendor, string Password);
        Task<bool> CheckAccountPassword(Vendor vendor, string password);
        int GenerateConfirmEmailToken();
        Task<bool> DeleteUserToken(ConfirmEmailToken token);
        Task<ConfirmEmailToken> retrieveUserToken(string userid);
        Task<ConfirmEmailToken> SaveGenerateConfirmEmailToken(ConfirmEmailToken emailToken);
        Task<bool> CheckEmailConfirmed(Vendor vendor);
        Task<bool> AddRoleAsync(Vendor vendor, string Role);
        Task<string> ForgotPassword(Vendor vendor);
        Task<bool> ConfirmEmail(string token, Vendor vendor);
        Task<bool> RemoveRoleAsync(Vendor vendor, IList<string> role);
        Task<resetPasswordDTO> ResetPasswordAsync(Vendor vendor, resetPasswordDTO resetPassword);
        Task<bool> RoleExist(string Role);
        Task<Vendor?> FindUserByEmailAsync(string email);
        Task<Vendor> FindUserByIdAsync(string id);
        Task<bool> UpdateUserInfo(Vendor applicationUser);
        Task<IList<string>> GetUserRoles(Vendor vendor);
        Task<bool> DeleteUserByEmail(Vendor vendor);
    }
}
