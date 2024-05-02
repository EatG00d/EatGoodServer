using EatGoodNaija.Server.Data;
using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Services.Interface;

namespace EatGoodNaija.Server.Services.Implementation
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepo;
        private readonly DataContext _context;

        public DishService(IDishRepository dishRepo)
        {
            _dishRepo = dishRepo;
        }

        public async Task<CustomerCart> AddDishToCartAsync(string dishId)
        {
            var dishToAdd = await _dishRepo.GetDishAsync(dishId);
            if (dishToAdd == null)
            {
                throw new ArgumentException("Dish not found.");
            }

            return await _dishRepo.AddDishAsync(dishToAdd);
        }

        public async Task<bool> DeleteDishFromCartAsync(string dishId)
        {
            var dishToDelete = await _dishRepo.GetDishAsync(dishId);
            if (dishToDelete == null)
            {
                throw new ArgumentException("Dish not found and so cannot be deleted.");
            }

            return await _dishRepo.DeleteDishAsync(dishId);
        }

        public async Task<CustomerCart> GetCartDishAsync(string dishId)
        {
            return await _dishRepo.GetDishAsync(dishId);
        }
    }
}
