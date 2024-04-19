﻿using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IbulakStoreServer.Services
{
    public class ProductService
    {
        private readonly StoreDbContext _context;
        public ProductService(StoreDbContext context)
        {
            _context=context;
        }
        public Product? Get(int id)
        {
            Product? product =  _context.Products.Find(id);
            return product;
        }
        public async Task<Product?> GetAsync(int id)
        {
            Product? product =await _context.Products.FindAsync(id);
            return product;
        }
        public async Task<List<Product>> GetsAsync()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return products;
        }
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Product product)
        {
            Product? oldProduct = await _context.Products.FindAsync(product.Id);
            if (oldProduct is null)
            {
                throw new Exception("محصولی با این شناسه پیدا نشد.");
            }
            oldProduct.Price=product.Price;
            oldProduct.Name=product.Name;
            oldProduct.Description = product.Description;
            oldProduct.ImageFileName = product.ImageFileName;
            _context.Products.Update(oldProduct);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                throw new Exception("محصولی با این شناسه پیدا نشد.");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
