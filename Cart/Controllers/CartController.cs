using Business.HelperModels;
using Business.ServicesInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {          
            this.cartService = cartService;
        }

        [HttpGet, Authorize(Roles = "Viewer,Standard")]
        public async Task<IActionResult> GetCartOverview()
        {
            var cartOverview = await cartService.GetCartOverviewAsync();

            if (cartOverview == null)
           
                return NotFound();
  
            return Ok(cartOverview);
        }

        [HttpGet("{cartItemId}"), Authorize(Roles = "Viewer,Standard")]
        public async Task<IActionResult> GetCartItemById([FromRoute] int cartItemId)
        {
            if(cartItemId <= 0)

                return BadRequest();

            var cartItemJson = await cartService.GetCartItemByIdAsync(cartItemId);

            if (cartItemJson == null)
           
                return NotFound();

            return Ok(cartItemJson);
        }

        [HttpPost, Authorize(Roles = "Standard")]
        public async Task<IActionResult> AddCartItem([FromBody] CartItemFromBody cartItemFromBody)
        {
            var cartItem = await cartService.AddCartItemAsync(cartItemFromBody);

            if (cartItem == null)
            
                return NotFound();

            return CreatedAtAction(nameof(GetCartItemById), new { controller = "Cart", cartItemId = cartItem.Id}, JsonSerializer.Serialize(cartItem));
        }

        [HttpDelete("{cartItemId}"), Authorize(Roles = "Standard")]
        public async Task<IActionResult> DeleteCartItemById([FromRoute] int cartItemId)
        {
            if (cartItemId <= 0)

                return BadRequest();

            var isdeleted = await cartService.DeleteCartItemAsync(cartItemId);

            if(isdeleted == false)
            {              
                return NotFound();
            }

            return Ok();
        }

        [HttpPatch("Cancel"), Authorize(Roles = "Standard")]
        public async Task<IActionResult> CancelCart()
        {
            var isCancelled = await cartService.CancelCartAsync();

            if (isCancelled == false)
            
                return NotFound();
       
            return Ok();     
        }

        [HttpPatch("Submit"), Authorize(Roles = "Standard")]
        public async Task<IActionResult> SubmitCart()
        {
            var isSubmitted = await cartService.SubmitCartAsync();

            if (isSubmitted == false)
           
                return NotFound();
      
            return Ok();
        }
    }
}
