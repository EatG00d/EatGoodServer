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

      
        ////private readonly RoleManager<IdentityRole> _roleManager;
        ////private readonly UserProfileCreateDTO _userProfileCreateDTO;
        ////private readonly UserProfileUpdateDTO _userProfileUpdateDTO;

        //public UserRepository(UserManager<UserProfile> usermanager /*rolemanager<identityrole> rolemanager, datacontext context, userprofilecreatedto usercreate, userprofileupdatedto userupdate*/)
        //{
        //    ////    _context = context;
        //    _userManager = /*usermanager*/;
        //    ////    //_roleManager = roleManager;
        //    ////    //_userProfileCreateDTO = userCreate;
        //    ////    //_userProfileUpdateDTO = userUpdate;

        //}

        public async Task<UserProfile> GetById(int userId)
        {
            return await _context.UserProfile.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task UpdateProfile(UserProfileUpdateDTO user, string UserId)
        { 
            _context.UserProfile.Find(UserId);
            await _context.SaveChangesAsync();
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

        public Task UpdateProfile(UserProfile user)
        {
            throw new NotImplementedException();
        }

    }

}
