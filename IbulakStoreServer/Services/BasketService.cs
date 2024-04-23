using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Basket?> GetAsync(int id)
        {
            return await _context.Baskets.FindAsync(id);
        }

        public async Task<List<Basket>> GetAllAsync()
        {
            return await _context.Baskets.ToListAsync();
        }


        public async Task<List<Basket>> GetByProductAsync(int productId)
        {
            return await _context.Baskets.Where(basket => basket.ProductId == productId).ToListAsync();
        }
        public async Task AddAsync(Basket basket)
        {
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Basket>> GetByUserAsync(int userId)
        {
            return await _context.Baskets.Where(basket => basket.UserId == userId).ToListAsync();
        }

        public async Task AddUserAsync(Basket basket)
        {
            var user = await _context.Users.FindAsync(basket.UserId);
            if (user is null)
            {
                throw new Exception("کاربری یافت نشد.");
            }
            basket.User = user;
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
        }

        public async Task AddProductAsync(Basket basket)
        {
            var product = await _context.Products.FindAsync(basket.ProductId);
            if (product is null)
            {
                throw new Exception("بسته ای یافت نشد.");
            }
            basket.Product = product;
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Basket basket)
        {
            var oldBasket = await _context.Baskets.FindAsync(basket.Id);
            if (oldBasket is null)
            {
                throw new Exception("بسته ای یافت نشد.");
            }
            oldBasket.Count = basket.Count;
            oldBasket.UserId = basket.UserId;
            oldBasket.ProductId = basket.ProductId;
            _context.Baskets.Update(oldBasket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var basket = await _context.Baskets.FindAsync(id);
            if (basket is null)
            {
                throw new Exception("بسته ای یافت نشد.");
            }
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
        }
    }
}
