using IbulakStoreServer.Data.Entities;
using IbulakStoreServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Bascket;
using Shared.Models.Baskets;
using Shared.Models.Product;
using Shared.Models.User;


namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketService _basketService;
        private readonly ProductService _productService;

        public BasketController(BasketService basketService, ProductService productService)
        {
            _basketService = basketService;
            _productService = productService;
        }
        [Authorize(Roles = "Admin")]

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _basketService.GetAsync(id);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _basketService.GetsAsync();
            return Ok(result);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(BascketAddRequestDto basket)
        {
            var product = await _productService.FindByIdAsync(basket.ProductId);
            if (product==null)
            {
                return NotFound();
            }
            if (product.Count<basket.Count)
            {
                return BadRequest("این تعداد کالا موجود نمی باشد");
            }
            await _basketService.AddAsync(basket);
            return Ok();
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Basket basket)
        {
            await _basketService.EditAsync(basket);
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _basketService.DeleteAsync(id);
            return Ok();
        }
        [Authorize]
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchRequestDto model)
        {
            var result = await _basketService.SearchAsync(model);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("BasketReportByUser")]
        public async Task<IActionResult> BasketReportByUser([FromQuery] BasketReportByUserRequestDto model)
        {
            var result = await _basketService.BasketReportByUserAsync(model);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetByUser([FromQuery] UserAddRequest model)
        {
            var result = await _basketService.GetByUserAsync(model);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetByProduct")]
        public async Task<IActionResult> GetByProduct([FromQuery] ProductRequestDto model)
        {
            var result = await _basketService.GetByProductAsync(model);
            return Ok(result);
        }

    }
}
