using EatGoodNaija.Server.Data;
using EatGoodNaija.Server.Model.DTO;
using EatGoodNaija.Server.Model.DTO.profileDTO;
using EatGoodNaija.Server.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EatGoodNaija.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly DataContext _dbContext;

        public ProfileController(IUserRepository userRepository, DataContext Context)
        {
            _userRepository = userRepository;
            _dbContext = Context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserProfile(UserProfileUpdateDTO profileUpdateDTO, string userId)
        {
            if (userId != profileUpdateDTO. Id)
            {
                return BadRequest("User ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepository.GetById(userId);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Update user profile properties
            existingUser.FullName = profileUpdateDTO.FullName;
            existingUser.Email = profileUpdateDTO.Email;
            existingUser.PhoneNumber = profileUpdateDTO.PhoneNumber;
            existingUser.HomeAddress = profileUpdateDTO.HomeAddress;
            existingUser.City = profileUpdateDTO.City;

            await _userRepository.UpdateProfile(profileUpdateDTO, userId);

            return NoContent();
        }



        [HttpPost]
        public async Task<IActionResult> CreateUserProfile(UserProfileCreateDTO profileCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //// Map DTO to User entity
            var newUser = new User
            {
                FullName = profileCreateDTO.FullName,
                Email = profileCreateDTO.Email,
                PhoneNumber = profileCreateDTO.PhoneNumber,
                HomeAddress = profileCreateDTO.HomeAddress,
                City = profileCreateDTO.City
            };

            await _userRepository.Add(newUser);

            return CreatedAtAction(nameof(GetUserProfile), new { userId = newUser.Id }, newUser);
        }
    }
}
