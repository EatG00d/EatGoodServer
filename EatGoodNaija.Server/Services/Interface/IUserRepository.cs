using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Model.DTO;
using EatGoodNaija.Server.Model.DTO.profileDTO;
namespace EatGoodNaija.Server.Services.Interface
{
    public interface IUserRepository
    {
        Task<UserProfile> GetById(int userId);
        Task UpdateProfile(UserProfile user);
        Task<string> Add(UserProfileCreateDTO userRequest);

    }
}
