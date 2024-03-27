using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Services.Interface;
using StackExchange.Redis;
using System.Text.Json;

namespace EatGoodNaija.Server.Services.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;
        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCartAsync(string cartId)
        {
            return await _database.KeyDeleteAsync(cartId);
        }

        public async Task<CustomerCart> GetCartAsync(string cartId)
        {
            var cartData = await _database.StringGetAsync(cartId);
            return cartData.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerCart>(cartData);
        }

        public async Task<CustomerCart> UpdateCartAsync(CustomerCart cart)
        {
            var itemCreated = await _database.StringSetAsync(cart.Id,
                JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));

            if (!itemCreated) return null;

            return await GetCartAsync(cart.Id);
        }

    }
}
