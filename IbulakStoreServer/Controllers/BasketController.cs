using IbulakStoreServer.Data.Entities;
using IbulakStoreServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketService _basketService;

        public BasketController(BasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasketsByUserId(int userId)
        {
            var baskets = await _basketService.GetBasketsByUserIdAsync(userId);
            return Ok(baskets);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket([FromBody] Basket basket)
        {
            await _basketService.AddBasketAsync(basket);
            return Ok();
        }

        [HttpPut("{basketId}")]
        public async Task<IActionResult> UpdateBasket(int basketId, [FromBody] Basket basket)
        {
            if (basketId != basket.Id)
            {
                return BadRequest();
            }

            await _basketService.UpdateBasketAsync(basket);
            return NoContent();
        }

        [HttpDelete("{basketId}")]
        public async Task<IActionResult> DeleteBasket(int basketId)
        {
            await _basketService.DeleteBasketAsync(basketId);
            return NoContent();
        }
    }
}
