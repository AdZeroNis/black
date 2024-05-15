using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Order;
using Shared.Models.Orders;

namespace IbulakStoreServer.Services
{
    public class OrderService
    {
        private readonly StoreDbContext _context;
        private List<Order> order;

        public int ProductId { get; private set; }

        public OrderService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Order?> GetAsync(int id)
        {
            Order? order = await _context.Orders.FindAsync(id);
            return order;
        }
        public async Task<List<Order>> GetsAsync()
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            return orders;
        }
        public async Task<List<Order>> GetsByProductAsync(int productId)
        {
            List<Order> orders = await _context.Orders.Where(order => order.ProductId == productId).ToListAsync();
            return order;
        }
        public async Task<List<Order>> GetsByUserAsync(int userId)
        {
            List<Order> orders = await _context.Orders.Where(order => order.UserId == userId).ToListAsync();
            return order;
        }
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(List<OrderAddRequestDto> orders)
        {
            var order = orders.Select(orderDto => new Order
            {

                Count = orderDto.Count,
                Price = orderDto.Price,
                ProductId = orderDto.ProductId,
                UserId = orderDto.UserId,
                CreatedAt = DateTime.Now
            }).ToList();

            _context.Orders.AddRange(order);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Order order)
        {
            Order? oldOrder = await _context.Orders.FindAsync(order.Id);
            if (oldOrder is null)
            {
                throw new Exception("سفارشی  با این شناسه پیدا نشد.");
            }
            oldOrder.Count = order.Count;
            oldOrder.Count = order.Price;
            oldOrder.ProductId = order.ProductId;
            oldOrder.UserId = order.UserId;

            _context.Orders.Update(oldOrder);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Order? order = await _context.Orders.FindAsync(id);
            if (order is null)
            {
                throw new Exception("سفارشی  با این شناسه پیدا نشد.");
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
        public async Task<List<SearchResponseDto>> SearchAsync(SearchRequestDto model)
        {
            var orders = await _context.Orders
               .Where(o =>
                    (model.Count == null || o.Count <= model.Count) &&
                    (model.FromDate == null || o.CreatedAt >= model.FromDate) &&
                    (model.ToDate == null || o.CreatedAt <= model.ToDate) &&
                    (!string.IsNullOrEmpty(model.UserName) ? o.User.UserName == model.UserName : true) &&
                    (!string.IsNullOrEmpty(model.ProductName) ? o.Product.Name == model.ProductName : true)
                )
               .Skip((model.PageNo - 1) * model.PageSize)
               .Take(model.PageSize)
               .Select(o => new SearchResponseDto
               {
                   ProductId = o.ProductId,
                   ProductName = o.Product.Name,
                   Count = o.Count,
                   Price = o.Price,
                   CreatedAt = o.CreatedAt,
                   ProductImageFileName = "YourLogicToGetImageFileName",
                   UserId = o.UserId,
                   UserName = o.User.UserName,
                   UserLastName = o.User.LastName
               })
               .ToListAsync();

            return orders;
        }
    }
}

