using IbulakStoreServer.Controllers;
using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace IbulakStoreServer.Services
{
    public class BasketService
    {
        private readonly StoreDbContext _context;
#pragma warning disable CS0649 // Field 'BasketService.basket' is never assigned to, and will always have its default value null
        private List<Basket> basket;
#pragma warning restore CS0649 // Field 'BasketService.basket' is never assigned to, and will always have its default value null

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
        public async Task AddAsync(BasketAddRequestDto model)
        {
            Basket basket = new Basket
            {
                UserId = model.UserId,
                Count = model.Count,
                ProductId = model.ProductId,
            };
            _context.Baskets.Update(basket);
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
    }
}
