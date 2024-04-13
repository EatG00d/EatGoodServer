using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Model.DTO;
using EatGoodNaija.Server.Model.DTO.profileDTO;
using System.Threading.Tasks;
namespace EatGoodNaija.Server.Services.Interface
{
    public interface IUserRepository
    {
        Task<UserProfile> GetById(string userId);
        Task<string> UpdateProfile(UserProfileUpdateDTO user, string UserId);
        Task<string> Add(UserProfileCreateDTO userRequest);
    }
}
