using EatGoodNaija.Server.Model.DTO;
using EatGoodNaija.Server.Services.Interface;

namespace EatGoodNaija.Server.Services.Implementation
{
    public class FoodOrderService : IFoodOrderService
    {
        private readonly IFoodOrderRepository _repository;

        public FoodOrderService(IFoodOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderDto>> GetOrdersForCustomerAsync(string customerId, int page, int pageSize)
        {
            return await _repository.GetOrdersForCustomerAsync(customerId, page, pageSize);
        }

        public async Task<OrderDto> ReorderAsync(string customerId, string orderId)
        {
            var order = await _repository.ReorderAsync(customerId, orderId);
            return new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Items = order.Items.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    FoodItemId = item.FoodItemId,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }

}
