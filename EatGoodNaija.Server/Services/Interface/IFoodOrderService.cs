using EatGoodNaija.Server.Model.DTO;

namespace EatGoodNaija.Server.Services.Interface
{
    public interface IFoodOrderService
    {
        Task<List<OrderDto>> GetOrdersForCustomerAsync(string customerId, int page, int pageSize);
        Task<OrderDto> ReorderAsync(string customerId, string orderId);
    }

}
