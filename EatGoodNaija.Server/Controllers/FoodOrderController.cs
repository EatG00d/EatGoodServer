using EatGoodNaija.Server.Model.DTO;
using EatGoodNaija.Server.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EatGoodNaija.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodOrderController : ControllerBase
    {
        private readonly FoodOrderService _service;

        public FoodOrderController(FoodOrderService service)
        {
            _service = service;
        }



        [HttpGet("{customerId}/orders")]
        public async Task<ActionResult<ResponseDTO<List<OrderDto>>>> GetOrdersForCustomerAsync(string customerId, int page = 1, int pageSize = 10)
        {
            var response = new ResponseDTO<List<OrderDto>>();
            try
            {
                var orders = await _service.GetOrdersForCustomerAsync(customerId, page, pageSize);
                response.StatusCode = 200;
                response.DisplayMessage = "Orders retrieved successfully";
                response.Result = orders;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.DisplayMessage = "An error occurred while processing the request.";
                response.ErrorMessage = new List<string> { ex.Message };
                return StatusCode(500, response);
            }
        }

        [HttpPost("{customerId}/reorder/{orderId}")]
        public async Task<ActionResult<ResponseDTO<OrderDto>>> ReorderAsync(string customerId, string orderId)
        {
            var response = new ResponseDTO<OrderDto>();
            try
            {
                var order = await _service.ReorderAsync(customerId, orderId);
                response.StatusCode = 200;
                response.DisplayMessage = "Order reordered successfully";
                response.Result = order;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.DisplayMessage = "An error occurred while processing the request.";
                response.ErrorMessage = new List<string> { ex.Message };
                return StatusCode(500, response);
            }
        }
    }
}
