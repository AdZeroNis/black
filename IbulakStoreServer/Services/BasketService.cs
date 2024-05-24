using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Shared.Models.Bascket;
using Shared.Models.Baskets;
using Shared.Models.Orders;
using System.Reflection.Metadata;
using Shared.Models.User;
using Shared.Models.Product;

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
        //سرج در سبد خرید
        public async Task<List<Shared.Models.Baskets.SearchResponseDto>> SearchAsync(Shared.Models.Baskets.SearchRequestDto model)
        {
            IQueryable<Basket> baskets = _context.Baskets
               .Where(a =>
           
                                (model.FullName == null || a.User.FullName.Contains(model.FullName))
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
                   
                   Description = a.Product.Description,
                   FullName = a.User.FullName,
                   ProductImageFileName = a.Product.ImageFileName
               })
               .ToListAsync();

            return searchResults;
        }

        ///گرقتن تعداد کالای هر کاربر و تعداد کالاهای هرکاربر
        public async Task<List<BasketReportByUsertResponseDto>> BasketReportByUserAsync(BasketReportByUserRequestDto model)
        {

            var userBaskets = await _context.Baskets
               .Where(b => b.UserId == model.UserId)
               .GroupBy(b => b.ProductId)
               .Select(g => new
               {
                   g.Key, // ProductId
                   Count = g.Sum(b => b.Count),
                   Product = g.First().Product
               })
               .ToListAsync();


            var report = userBaskets.Select(a => new BasketReportByUsertResponseDto
            {
                UserId = model.UserId,

                ProductId = a.Product.Id,
                ProductName = a.Product.Name,
                Count = a.Count,

                TotalSum = a.Product.Price * a.Count
            }).ToList();

            return report;
        }
        public async Task<List<UserAddResponse>> GetByUserAsync(UserAddRequest model)
        {
            var userId = model.UserId;

            var user = await _context.Users.FindAsync(userId);

            if (user is null)
            {
                throw new Exception("User not found.");
            }


            var groupedOrders = await _context.Baskets
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

            var groupedBasket = await _context.Baskets
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





    }
}



