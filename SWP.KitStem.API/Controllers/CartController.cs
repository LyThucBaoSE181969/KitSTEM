using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.KitStem.API.ResponseModel;
using SWP.KitStem.Service.Services;

namespace SWP.KitStem.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("carts")]
        public async Task<ActionResult<IEnumerable<CartResponseModel>>> GetCarts()
        {
            var carts = await _cartService.GetCartAsync();
            var response = carts.Select(cart => new CartResponseModel
            {
                UserId = cart.UserId,
                PackageId = cart.PackageId,
                PackageQuantity = cart.PackageQuantity,
            });

            return Ok(response);
        }
    }
}
