using EatGoodNaija.Server.Data;
using EatGoodNaija.Server.Model;
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

        public ProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfile>>  GetUserProfile(string userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUserProfile(UserProfileUpdateDTO profileUpdateDTO, string userId)
        {
            var result = await _userRepository.UpdateProfile(profileUpdateDTO, userId);
            if (result == "User not found")
            {
                return NotFound();
            }
            return Ok(result);
        }




        [HttpPost]
        public async Task<IActionResult> CreateUserProfile(UserProfileCreateDTO profileCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var response = await _userRepository.Add(profileCreateDTO);

            return Ok(response);
        }
    }
}
