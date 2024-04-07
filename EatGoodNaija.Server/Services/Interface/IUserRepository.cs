using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Model.DTO;
using EatGoodNaija.Server.Model.DTO.profileDTO;
namespace EatGoodNaija.Server.Services.Interface
{
    public interface IUserRepository
    {
        Task<UserProfile> GetById(int userId);
        Task<string> UpdateProfile(UserProfileUpdateDTO user, int UserId);
        Task<string> Add(UserProfileCreateDTO userRequest);

    }
}
