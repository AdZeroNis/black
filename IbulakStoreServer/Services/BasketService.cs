using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Shared.Models.Bascket;
using Shared.Models.Baskets;
using Shared.Models.Orders;
using System.Reflection.Metadata;

namespace IbulakStoreServer.Services
{
    public class BasketService
    {
        private readonly StoreDbContext _context;
        private List<Basket> basket;

        public int ProductId { get; private set; }

        public BasketService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Basket?> GetAsync(int id)
        {
            Basket? basket = await _context.Baskets.FindAsync(id);
            return basket;
        }
        public async Task<List<Basket>> GetsAsync()
        {
            List<Basket> baskets = await _context.Baskets.ToListAsync();
            return baskets;
        }
        public async Task<List<Basket>> GetsByProductAsync(int productId)
        {
            List<Basket> baskets = await _context.Baskets.Where(basket => basket.ProductId == ProductId).ToListAsync();
            return basket;
        }
        public async Task<List<Basket>> GetsByUserAsync(int userId)
        {
            List<Basket> baskets = await _context.Baskets.Where(basket => basket.UserId == userId).ToListAsync();
            return basket;
        }
        public async Task AddAsync(BascketAddRequestDto model)
        {
            Basket basket = new Basket
            {
                UserId = model.UserId,
                Count = model.Count,
                ProductId = model.ProductId,
            };
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Basket basket)
        {
            Basket? oldBasket = await _context.Baskets.FindAsync(basket.Id);
            if (oldBasket is null)
            {
                throw new Exception("سبد خریدی  با این شناسه پیدا نشد.");
            }
            oldBasket.Count = basket.Count;
            oldBasket.ProductId = basket.ProductId;
            oldBasket.UserId = basket.UserId;

            _context.Baskets.Update(oldBasket);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Basket? basket = await _context.Baskets.FindAsync(id);
            if (basket is null)
            {
                throw new Exception("سبد خریدی  با این شناسه پیدا نشد.");
            }
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Shared.Models.Baskets.SearchResponseDto>> SearchAsync(Shared.Models.Baskets.SearchRequestDto model)
        {
            IQueryable<Basket> baskets = _context.Baskets
               .Where(a =>
            (model.Count == null || a.Count <= model.Count)
                               && (model.UserName == null || a.User.Name.Contains(model.UserName))
                               && (model.ProductName == null || a.Product.Name.Contains(model.ProductName))
                               && (model.ProductCount == null || a.Product.Count <= model.ProductCount)
                               );

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                switch (model.SortBy)
                {
                    case "CountAsc":
                        baskets = baskets.OrderBy(a => a.Count);
                        break;
                    case "CountDesc":
                        baskets = baskets.OrderByDescending(a => a.Count);
                        break;
                }
            }

            baskets = baskets.Skip(model.PageNo * model.PageSize).Take(model.PageSize);

            var searchResults = await baskets
               .Select(a => new Shared.Models.Baskets.SearchResponseDto
               {
                   BasketId = a.Id,
                   ProductId = a.Product.Id,
                   ProductName = a.Product.Name,
                   UserId = a.UserId,
                   Count = a.Count,
                   ProductCount = a.Product.Count,
                   Description = a.Product.Description,
                   UserName = a.User.Name,
                   UserLastName = a.User.LastName,
                   ProductImageFileName = a.Product.ImageFileName
               })
               .ToListAsync();

            return searchResults;
        }

     
 public async Task<List<BasketReportByUsertResponseDto>> BasketReportByUserAsync(BasketReportByUserRequestDto model)
        {
            // Query to get all baskets for the given user
            var userBaskets = await _context.Baskets
               .Where(b => b.UserId == model.UserId)
               .GroupBy(b => b.ProductId) // Group by ProductId
               .Select(g => new
               {
                   g.Key, // ProductId
                   Count = g.Sum(b => b.Count), // Sum of counts for each product
                   Product = g.First().Product // Assuming each product ID is unique within the user's basket
               })
               .ToListAsync();

            // Convert to DTOs and calculate total sum for each product
            var report = userBaskets.Select(a => new BasketReportByUsertResponseDto
            {
                UserId = (int)model.UserId,
                ProductId = a.Product.Id,
                ProductName = a.Product.Name,
                Count = a.Count,
                TotalSum = a.Product.Price * a.Count // Assuming Product has a Price property
            }).ToList();

            return report;
        }



    }





}



