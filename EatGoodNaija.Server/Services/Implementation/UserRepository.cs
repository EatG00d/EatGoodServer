using EatGoodNaija.Server.Data;
using EatGoodNaija.Server.Model.DTO.profileDTO;
using EatGoodNaija.Server.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EatGoodNaija.Server.Model;

namespace EatGoodNaija.Server.Services.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<UserProfile> _userManager;

        public UserRepository(DataContext context, UserManager<UserProfile> usermanager)
        {
            _context = context;
            _userManager = usermanager;
            
        }

        public async Task<UserProfile> GetById(string userId)
        {
            return await _context.UserProfile.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<string> UpdateProfile(UserProfileUpdateDTO user, string userId)
        {
            var userProfile = await _context.UserProfile.FirstOrDefaultAsync(u => u.UserId == userId);

            if (userProfile == null)
            {
                return "User not found";
            }

            userProfile.FullName = user.FullName;
            userProfile.Email = user.Email;
            userProfile.PhoneNumber = user.PhoneNumber;
            userProfile.HomeAddress = user.HomeAddress;
            userProfile.City = user.City;

            _context.UserProfile.Update(userProfile);
            await _context.SaveChangesAsync();

            return "User update successful";
        }

        public async Task<string> Add(UserProfileCreateDTO userRequest)
        {
            var user = await _userManager.FindByEmailAsync(userRequest.Email);
            if (user != null)
            {
                return "User already exists";
            }

            var newUserProfile = new UserProfile
            {
                FullName = userRequest.FullName,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,
                HomeAddress = userRequest.HomeAddress,
                City = userRequest.City
            };

            _context.UserProfile.Add(newUserProfile);
            await _context.SaveChangesAsync();
            return "User profile added successfully";
        }



    }

}
