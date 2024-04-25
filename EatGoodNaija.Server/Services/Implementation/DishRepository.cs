using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Services.Interface;
using StackExchange.Redis;
using System.Text.Json;

namespace EatGoodNaija.Server.Services.Implementation
{
    public class DishRepository : IDishRepository
    {
        private readonly IDatabase _cartDatabase;

        public DishRepository(IConnectionMultiplexer redis)
        {
            _cartDatabase = redis.GetDatabase();
        }

        public async Task<CustomerCart> GetDishAsync(string dishId)
        {
            var cartDish = await _cartDatabase.StringGetAsync(dishId);
            return cartDish.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerCart>(cartDish);
        }

        public async Task<CustomerCart> AddDishAsync(CustomerCart dish)
        {
            var dishAdded = await _cartDatabase.StringSetAsync(dish.Id,
                JsonSerializer.Serialize(dish), TimeSpan.FromDays(30));

            if (!dishAdded) return null;

            return await GetDishAsync(dish.Id);
        }

        public async Task<bool> DeleteDishAsync(string dishId)
        {
            return await _cartDatabase.KeyDeleteAsync(dishId);
        }


    }
}
