using IbulakStoreServer.Data.Entities;
using IbulakStoreServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Order;

namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _orderService.GetAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _orderService.GetsAsync();
            return Ok(result);
        }
        [HttpGet("GetsByProduct")]
        public async Task<IActionResult> GetsByProduct(int productId)
        {
            var result = await _orderService.GetsByProductAsync(productId);
            return Ok(result);
        }
        [HttpGet("GetsByUser")]
        public async Task<IActionResult> GetsByUser(int userId)
        {
            var result = await _orderService.GetsByUserAsync(userId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Order order)
        {
            await _orderService.AddAsync(order);
            return Ok();
        }
        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(List<OrderAddRequestDto> orders)
        {
            await _orderService.AddRangeAsync(orders);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Order order)
        {
            await _orderService.EditAsync(order);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteAsync(id);
            return Ok();
        }
    }
}
