using EatGoodNaija.Server.Data;
using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Model.DTO.authenticationDTO;
using EatGoodNaija.Server.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EatGoodNaija.Server.Services.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<Vendor> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public AuthRepository(UserManager<Vendor> userManager, RoleManager<IdentityRole> roleManager, DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<bool> AddRoleAsync(Vendor vendor, string Role)
        {
            var AddRole = await _userManager.AddToRoleAsync(vendor, Role);
            if (AddRole.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckAccountPassword(Vendor vendor, string password)
        {
            var checkUserPassword = await _userManager.CheckPasswordAsync(vendor, password);
            return checkUserPassword;
        }

        public async Task<bool> CheckEmailConfirmed(Vendor vendor)
        {
            var checkConfirm = vendor.EmailConfirmed == true;
            return checkConfirm;
        }

        public async Task<bool> ConfirmEmail(string token, Vendor vendor)
        {
            var result = await _userManager.ConfirmEmailAsync(vendor, token);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserByEmail(Vendor vendor)
        {
            var result = await _userManager.DeleteAsync(vendor);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserToken(ConfirmEmailToken token)
        {
            _context.ConfirmEmailTokens.Remove(token);
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Vendor?> FindUserByEmailAsync(string email)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser == null)
            {
                return null;
            }
            return findUser;
        }

        public async Task<Vendor> FindUserByIdAsync(string id)
        {
            var findUser = await _userManager.FindByIdAsync(id);
            return findUser;
        }

        public async Task<string> ForgotPassword(Vendor vendor)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(vendor);
            return token;
        }

        int IAuthRepository.GenerateConfirmEmailToken()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 1000000);
            return randomNumber;
        }

        public async Task<IList<string>> GetUserRoles(Vendor vendor)
        {
            var getRoles = await _userManager.GetRolesAsync(vendor);
            if (getRoles != null)
            {
                return getRoles;
            }
            return null;
        }

        public async Task<bool> RemoveRoleAsync(Vendor vendor, IList<string> role)
        {
            var removeRole = await _userManager.RemoveFromRolesAsync(vendor, role);
            if (removeRole.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<resetPasswordDTO> ResetPasswordAsync(Vendor vendor, resetPasswordDTO resetPassword)
        {
            var result = await _userManager.ResetPasswordAsync(vendor, resetPassword.Token, resetPassword.Password);
            if (result.Succeeded)
            {
                return resetPassword;
            }
            return null;
        }

        public async Task<ConfirmEmailToken> retrieveUserToken(string userid)
        {
            return await _context.ConfirmEmailTokens.FirstOrDefaultAsync(u => u.VendorId == userid);

        }

        public async Task<bool> RoleExist(string Role)
        {
            var check = await _roleManager.RoleExistsAsync(Role);
            return check;
        }

        public async Task<ConfirmEmailToken> SaveGenerateConfirmEmailToken(ConfirmEmailToken emailToken)
        {
            var saveToken = await _context.ConfirmEmailTokens.AddAsync(emailToken);
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return emailToken;
            }
            return null;
        }

        public async Task<Vendor> SignUpAsync(Vendor vendor, string Password)
        {
            var result = await _userManager.CreateAsync(vendor, Password);
            if (result.Succeeded)
            {
                return vendor;
            }
            return null;            
        }

        public async Task<bool> UpdateUserInfo(Vendor applicationUser)
        {
            var updateUserInfo = await _userManager.UpdateAsync(applicationUser);
            if (updateUserInfo.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
