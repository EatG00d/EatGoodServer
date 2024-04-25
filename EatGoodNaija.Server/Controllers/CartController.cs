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
        private readonly IDishService _dishService;

        public CartController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPost("addtocart")]
        public async Task<ActionResult<CustomerCart>> AddToCart(CustomerCart cart)
        {
            try
            {
                var addedToCart = await _dishService.AddDishToCartAsync(cart.Id);
                return Ok(addedToCart);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpGet("cart")]
        public async Task<ActionResult<CustomerCart>> GetCart(string id)
        {
            try
            {
                var cartItems = await _dishService.GetCartDishAsync(id);
                return Ok(cartItems ?? new CustomerCart(id));
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

      

        [HttpDelete("deletefromcart")]
        public async Task DeleteCartAsync(string id)
        {
            await _dishService.DeleteDishFromCartAsync(id);
        }
    }
}
