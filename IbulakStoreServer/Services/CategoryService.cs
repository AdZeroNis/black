using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IbulakStoreServer.Services
{
    public class CategoryService
    {
        private readonly StoreDbContext _context;
        public CategoryService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Category?> GetAsync(int id)
        {
            Category? category = await _context.Categories.FindAsync(id);
            return category;
        }
        public async Task<List<Category>> GetsAsync()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return categories;
        }
        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Category category)
        {
            Category? oldCategory = await _context.Categories.FindAsync(category.Id);
            if (oldCategory is null)
            {
                throw new Exception("دسته بندی محصولی با این شناسه پیدا نشد.");
            }
            oldCategory.Name = category.Name;
            oldCategory.ImageFileName = category.ImageFileName;
            _context.Categories.Update(oldCategory);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Category? category = await _context.Categories.FindAsync(id);
            if (category is null)
            {
                throw new Exception("دسته بندی محصولی با این شناسه پیدا نشد.");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
