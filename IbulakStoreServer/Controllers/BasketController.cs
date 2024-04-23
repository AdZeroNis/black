using IbulakStoreServer.Data.Entities;
using IbulakStoreServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // Corrected method name to match the service method
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _BasketService.GetAllAsync();
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
