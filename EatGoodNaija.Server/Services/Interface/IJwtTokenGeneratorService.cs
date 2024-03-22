using EatGoodNaija.Server.Model;

namespace EatGoodNaija.Server.Services.Interface
{
    public interface IJwtTokenGeneratorService
    {
        Task <string> GenerateToken(Vendor vendor);
    }
}
