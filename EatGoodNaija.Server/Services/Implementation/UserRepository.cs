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

        public async Task<UserProfile> GetById(int userId)
        {
            return await _context.UserProfile.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<string> UpdateProfile(UserProfileUpdateDTO user, int UserId)
        { 
          var far = await  _context.UserProfile.FirstOrDefaultAsync(u => u.UserId == UserId);

            far.FullName = user.FullName;
            far.Email = user.Email;
            far.PhoneNumber = user.PhoneNumber;
            far.HomeAddress = user.HomeAddress;
            far.City = user.City;

            _context.UserProfile.Update(far);
            await _context.SaveChangesAsync();
            return "user update sucessful";
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
