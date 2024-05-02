using EatGoodNaija.Server.Model;

namespace EatGoodNaija.Server.Services.Interface
{
    public interface IDishService
    {
        Task<CustomerCart> AddDishToCartAsync(string dishId);
        Task<CustomerCart> GetCartDishAsync(string dishId);
        Task<bool> DeleteDishFromCartAsync(string dishId);
    }

}
