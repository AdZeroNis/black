using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using IbulakStoreServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Order;
using Shared.Models.Orders;


namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly StoreDbContext _context;


        public OrderController(OrderService orderService, StoreDbContext context)
        {
            _orderService = orderService;
            _context = context;
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
        // [HttpPost]
        // public async Task<IActionResult> Add(Order order)
        // {
        //    await _orderService.AddAsync(order);
        //    return Ok();
        // }
        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(List<OrderAddRequestDto> orders)
        {
            var orderEntities = new List<Order>();
            foreach (var orderDto in orders)
            {
                Product? product = await _context.Products.FindAsync(orderDto.ProductId);
                if (product is null)
                {
                    return NotFound($"محصولی با شناسه {orderDto.ProductId} پیدا نشد.");
                }

                if (orderDto.Count > product.Count)
                {
                    return BadRequest($"تعداد محصول درخواستی بیشتر از موجودی است. تعداد موجود: {product.Count}");
                }
                product.Count -= orderDto.Count;
                _context.Products.Update(product);

                var order = new Order
                {
                    Count = orderDto.Count,
                    Price = orderDto.Price,
                    ProductId = orderDto.ProductId,
                    UserId = orderDto.UserId,
                    CreatedAt = DateTime.Now
                };

                orderEntities.Add(order);
            }

            _context.Orders.AddRange(orderEntities);
            await _context.SaveChangesAsync();
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

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchRequestDto model)
        {
            var result = await _orderService.SearchAsync(model);
            return Ok(result);
        }
        [HttpGet("OrdersReportByProduct")]
        public async Task<IActionResult> OrdersReportByProduct([FromQuery] OrderReportByProductRequestDto model)
        {
            var result = await _orderService.OrdersReportByProductAsync(model);
            return Ok(result);
        }
        [HttpGet("OrdersTotalByProductName")]
        public async Task<IActionResult> OrdersTotalByProductName([FromQuery] OrdersTotalByProductNameRequestDto model)
        {
            var result = await _orderService.OrdersTotalByProductNameAsync(model);
            return Ok(result);
        }
       




    }
}
