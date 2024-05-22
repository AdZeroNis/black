﻿using IbulakStoreServer.Data.Domain;
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
        public async Task<List<Order>> GetsByUserAsync(string userId)
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
            IQueryable<Order> orders = _context.Orders
               .Where(a =>
            (model.Count == null || a.Count <= model.Count)
                               && (model.FromDate == null || a.CreatedAt >= model.FromDate)
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
                   OrderId=a.Id,
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
                    TotalSum = a.Sum(s => s.Price)
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
        public async Task<List<OrdersTotalByProductNameResponseDto>> OrdersTotalByProductNameAsync(OrdersTotalByProductNameRequestDto model)
        {
            // First, filter products by name if a product name is provided
            var filteredProducts = _context.Products
                                             .Where(p => model.ProductName == null || p.Name.Contains(model.ProductName));

            // Then, join orders with the filtered products to calculate the total sum for each product
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
                                                                     ProductName = g.FirstOrDefault().Product.Name // Set the ProductName property
                                                                 })
                                                                 .ToListAsync();

            // Finally, convert the results to DTOs
            var result = ordersWithFilteredProducts.Select(o => new OrdersTotalByProductNameResponseDto
            {
                ProductName = o.ProductName,
                ProductId = o.ProductId,
                TotalSum = o.TotalSum  // Ensure TotalSum has a default value
            }).ToList();

            // Apply pagination
            result = result.Skip((model.PageNo) * model.PageSize).Take(model.PageSize).ToList();

            return result;
        }



    }
}

