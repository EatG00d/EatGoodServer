using EatGoodNaija.Server.Model;

namespace EatGoodNaija.Server.Services.Interface
{
    public interface IDishRepository
    {
        Task<CustomerCart> AddDishAsync(CustomerCart dish);
        Task<CustomerCart> GetDishAsync(string dishId);
        Task<bool> DeleteDishAsync(string dishId);
    }
}
