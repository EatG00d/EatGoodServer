using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EatGoodNaija.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerCart>> GetCartById(string id)
        {
            var cart = await _cartRepository.GetCartAsync(id);
            return Ok(cart ?? new CustomerCart(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerCart>> UpdateCart(CustomerCart cart)
        {
            var updatedCart = await _cartRepository.UpdateCartAsync(cart);
            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task DeleteCartAsync(string id)
        {
            await _cartRepository.DeleteCartAsync(id);
        }
    }
}
