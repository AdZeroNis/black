using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IbulakStoreServer.Services
{
    public class BasketService
    {
        private readonly StoreDbContext _context;

        public BasketService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Basket>> GetBasketsByUserIdAsync(int userId)
        {
            return await _context.Baskets.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task AddBasketAsync(Basket basket)
        {
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBasketAsync(Basket basket)
        {
            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBasketAsync(int basketId)
        {
            var basket = await _context.Baskets.FindAsync(basketId);
            if (basket != null)
            {
                _context.Baskets.Remove(basket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
