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
            var result = await _service.GetOrdersForCustomerAsync(customerId, page, pageSize);
            return Ok(result);
        }

        [HttpPost("{customerId}/reorder/{orderId}")]
        public async Task<ActionResult<ResponseDTO<OrderDto>>> ReorderAsync(string customerId, string orderId)
        {
            var order = await _service.ReorderAsync(customerId, orderId);
            return Ok(order);
            
        }
    }
}
