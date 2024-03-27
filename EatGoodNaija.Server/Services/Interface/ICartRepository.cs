using EatGoodNaija.Server.Model;

namespace EatGoodNaija.Server.Services.Interface
{
    public interface ICartRepository
    {
        Task<CustomerCart> GetCartAsync(string cartId);
        Task<CustomerCart> UpdateCartAsync(CustomerCart cart);
        Task<bool> DeleteCartAsync(string cartId);
    }
}
