using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Model.DTO;

namespace EatGoodNaija.Server.Services.Interface
{
    public interface IFoodOrderRepository
    {
        //Task<Order> CreateOrderAsync(string customerId, List<CartItemDto> items);
        Task<List<OrderDto>> GetOrdersForCustomerAsync(string customerId, int page, int pageSize);
        Task<Order> ReorderAsync(string customerId, string orderId);
    }

}
