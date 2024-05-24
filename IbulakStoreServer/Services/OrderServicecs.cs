using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Order;
using Shared.Models.Orders;
using Shared.Models.Product;
using Shared.Models.User;
using System.Reflection.Metadata.Ecma335;

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
            IQueryable<Order> orders = _context.Orders
               .Where(a =>
            
                                (model.FromDate == null || a.CreatedAt >= model.FromDate)
                               && (model.ToDate == null || a.CreatedAt <= model.ToDate)
                               && (model.FullName == null || a.User.FullName.Contains(model.FullName))
                               && (model.ProductName == null || a.Product.Name.Contains(model.ProductName))
                               );

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                switch (model.SortBy)
                {
                    case "CountAsc":
                        orders = orders.OrderBy(a => a.Count);
                        break;
                    case "CountDesc":
                        orders = orders.OrderByDescending(a => a.Count);
                        break;
                }
            }

            orders = orders.Skip(model.PageNo * model.PageSize).Take(model.PageSize);

            var searchResults = await orders
               .Select(a => new SearchResponseDto
               {
                   OrderId = a.Id,
                   ProductId = a.Id,
                   ProductName = a.Product.Name,
                   UserId = a.UserId,
                   Count = a.Count,
                   Price = a.Price,
                   CreatedAt = a.CreatedAt,
                   Description = a.Product.Description,
                   FullName = a.User.FullName,
                   ProductImageFileName = a.Product.ImageFileName
               })
               .ToListAsync();

            return searchResults;
        }
        //فروش کالا در تاریخ مشخص
        public async Task<List<OrderReportByProductResponseDto>> OrdersReportByProductAsync(OrderReportByProductRequestDto model)
        {
            var ordersQuery = _context.Orders.Where(a =>
                                (model.FromDate == null || a.CreatedAt >= model.FromDate)
                               && (model.ToDate == null || a.CreatedAt <= model.ToDate)
                                )
                .GroupBy(a => a.ProductId)
                .Select(a => new
                {
                    ProductId = a.Key,
                    TotalSum = a.Sum(s => s.Count)
                });

            var productsQuery = from product in _context.Products
                                from order in ordersQuery.Where(a => a.ProductId == product.Id).DefaultIfEmpty()
                                select new OrderReportByProductResponseDto
                                {
                                    ProductName = product.Name,
                                    ProductCategoryName = product.Category.Name,
                                    ProductId = product.Id,
                                    TotalSum = (int?)order.TotalSum
                                };

            productsQuery = productsQuery.Skip(model.PageNo * model.PageSize)
                                .Take(model.PageSize);
            var result = await productsQuery.ToListAsync();
            return result;
        }
        //گرقتن اطلاعات فروش براساس اسم کالا
        public async Task<List<OrdersTotalByProductNameResponseDto>> OrdersTotalByProductNameAsync(OrdersTotalByProductNameRequestDto model)
        {

            var filteredProducts = _context.Products
                                             .Where(p => model.ProductName == null || p.Name.Contains(model.ProductName));


            var ordersWithFilteredProducts = await _context.Orders
                                                             .Join(filteredProducts,
                                                                   order => order.ProductId,
                                                                   product => product.Id,
                                                                   (order, product) => new { Order = order, Product = product })
                                                                 .GroupBy(x => x.Order.ProductId)
                                                                 .Select(g => new
                                                                 {
                                                                     ProductId = g.Key,
                                                                     TotalSum = g.Sum(x => x.Order.Price * x.Order.Count),
                                                                     ProductName = g.FirstOrDefault().Product.Name
                                                                 })
                                                                 .ToListAsync();


            var result = ordersWithFilteredProducts.Select(o => new OrdersTotalByProductNameResponseDto
            {
                ProductName = o.ProductName,
                ProductId = o.ProductId,
                TotalSum = o.TotalSum
            }).ToList();


            result = result.Skip((model.PageNo) * model.PageSize).Take(model.PageSize).ToList();

            return result;
        }
        // جمع فروش
        public async Task<List<OrderAllTotalResponseDto>> OrderTotalAsync(orderAllTotalRequestDto model)
        {
            var totalSum = await _context.Orders
                .Select(o => o.Price * o.Count)
                .SumAsync();

            var count = await _context.Orders.CountAsync();

            var result = new List<OrderAllTotalResponseDto>
    {
           new OrderAllTotalResponseDto
        {
            TotalSum = totalSum,
            Count = count
        }
    };

            result = result.Skip((model.PageNo) * model.PageSize).Take(model.PageSize).ToList();

            return result;
        }
        public async Task<List<UserAddResponse>> GetByUserAsync(UserAddRequest model)
        {
            var userId = model.UserId;

            var user = await _context.Users.FindAsync(userId);

            if (user is null)
            {
                throw new Exception("User not found.");
            }

 
            var groupedOrders = await _context.Orders
               .Where(o => o.UserId == userId)
               .GroupBy(o => o.UserId)
               .Select(g => g.First()) 
               .ToListAsync();

            var userResponses = groupedOrders.Select(o => new UserAddResponse
            {
                UserId = o.UserId,
                FullName = o.User.FullName
            }).ToList();

            return userResponses;
        }
        public async Task<List<ProductResponseDto>> GetByProductAsync(ProductRequestDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var productId = model.ProductId;


            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            var groupedBasket = await _context.Orders
               .Where(o => o.ProductId == productId)
               .GroupBy(o => o.ProductId)
               .Select(g => g.FirstOrDefault())
               .ToListAsync();


            groupedBasket.RemoveAll(item => item == null);

            var productResponses = groupedBasket.Where(o => o != null)
               .Select(o => new ProductResponseDto
               {
                   ProductId = o.ProductId,
                   Count = o.Count,
                   CreatedAt = o.CreatedAt,
                   Price = o.Product.Price,
                   ProductImageFileName = o.Product?.ImageFileName,
                   Description = o.Product?.Description,
                   ProductName = o.Product?.Name
               }).ToList();

            return productResponses;
        }

        ///گرقتن تعداد کالای هر کاربر و تعداد کالاهای هرکاربر
        public async Task<List<OrderReportByUsertResponseDto>> OrderReportByUserAsync(OrderReportByUserRequestDto model)
        {

            var userOeders = await _context.Orders
               .Where(b => b.UserId == model.UserId)
               .GroupBy(b => b.ProductId)
               .Select(g => new
               {
                   g.Key, // ProductId
                   Count = g.Sum(b => b.Count),
                   Product = g.First().Product
               })
               .ToListAsync();


            var report = userOeders.Select(a => new OrderReportByUsertResponseDto
            {
                UserId = model.UserId,

                ProductId = a.Product.Id,
                ProductName = a.Product.Name,
                Count = a.Count,

                TotalSum = a.Product.Price * a.Count
            }).ToList();

            return report;
        }



    }
}

