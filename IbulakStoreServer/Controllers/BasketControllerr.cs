using IbulakStoreServer.Data.Entities;
using IbulakStoreServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketService _BasketService;

        public BasketController(BasketService basketService)
        {
            _BasketService = basketService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _BasketService.GetAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _BasketService.GetsAsync();
            return Ok(result);
        }
        [HttpGet("GetsByProduct")]
        public async Task<IActionResult> GetsByProduct(int productId)
        {
            var result = await _BasketService.GetsByProductAsync(productId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Basket basket)
        {
            await _BasketService.AddAsync(basket);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Basket basket)
        {
            await _BasketService.EditAsync(basket);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _BasketService.DeleteAsync(id);
            return Ok();
        }
    }
}
