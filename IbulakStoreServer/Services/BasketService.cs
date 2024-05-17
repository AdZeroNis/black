using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Bascket;
using Shared.Models.Baskets;

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
        public async Task<List<SearchResponseDto>> SearchAsync(SearchRequestDto model)
        {
            IQueryable<Basket> baskets = _context.Baskets
               .Where(a =>
            (model.Count == null || a.Count <= model.Count)
                               && (model.UserName == null || a.User.Name.Contains(model.UserName))
                               && (model.ProductName == null || a.Product.Name.Contains(model.ProductName))
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
               .Select(a => new SearchResponseDto
               {
                   ProductId = a.Id,
                   ProductName = a.Product.Name,
                   UserId = a.UserId,
                   Count = a.Count,
                   Description = a.Product.Description,
                   UserName = a.User.Name,
                   UserLastName = a.User.LastName,
                   ProductImageFileName = a.Product.ImageFileName
               })
               .ToListAsync();

            return searchResults;
        }
    }
}
