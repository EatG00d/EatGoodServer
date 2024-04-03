using EatGoodNaija.Server.Model.DTO;

namespace EatGoodNaija.Server.Services.Interface
{
    public interface IFoodOrderService
    {
        Task<ResponseDTO<List<OrderDto>>> GetOrdersForCustomerAsync(string customerId, int page, int pageSize);
        Task<ResponseDTO<OrderDto>> ReorderAsync(string customerId, string orderId);
    }

}
