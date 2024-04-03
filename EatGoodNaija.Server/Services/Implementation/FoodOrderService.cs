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

        public async Task<ResponseDTO<List<OrderDto>>> GetOrdersForCustomerAsync(string customerId, int page, int pageSize)
        {
            var response = new ResponseDTO<List<OrderDto>>();
            
            try
            {
                if (string.IsNullOrEmpty(customerId))
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.DisplayMessage = "Invalid customer ID.";
                    return response;
                }

                var orders = await _repository.GetOrdersForCustomerAsync(customerId, page, pageSize);
                if (!orders.Any() || orders.Count == 0) 
                {
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.DisplayMessage = "Input an order";
                    response.Result = orders;
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Orders retrieved successfully";
                response.Result = orders;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.DisplayMessage = "An error occurred while processing the request.";
                response.ErrorMessage = new List<string> { ex.Message };
                return response;
            }
        }

        public async Task<ResponseDTO<OrderDto>> ReorderAsync(string customerId, string orderId)
        {
            var response = new ResponseDTO<OrderDto>();
            try
            {
                if (string.IsNullOrEmpty(customerId) || string.IsNullOrEmpty(orderId))
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.DisplayMessage = "Customer ID or Order ID is required.";
                    return response;
                }

                var order = await _repository.ReorderAsync(customerId, orderId);

                if (order == null)
                {
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.DisplayMessage = "Order not found.";
                    return response;
                }

                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Order reordered successfully";
                response.Result = new OrderDto
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
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.DisplayMessage = "An error occurred while processing the request.";
                response.ErrorMessage = new List<string> { ex.Message };
            }

            return response;

            
        }

    }

}
