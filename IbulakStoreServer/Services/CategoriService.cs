using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IbulakStoreServer.Services
{
    public class CategoriService
    {
        private readonly StoreDbContext _context;
        public CategoriService(StoreDbContext context)
        {
            _context = context;
        }
        public Categori? Get(int id)
        {
            Categori? categori = _context.Categoris.Find(id);
            return categori;
        }
        public async Task<Categori?> GetAsync(int id)
        {
            Categori? categori = await _context.Categoris.FindAsync(id);
            return categori;
        }
        public async Task<List<Categori>> GetsAsync()
        {
            List<Categori> categoris = await _context.Categoris.ToListAsync();
            return categoris;
        }
        public async Task AddAsync(Categori categori)
        {
            _context.Categoris.Add(categori);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Categori categori)
        {
            Categori? oldCategori = await _context.Categoris.FindAsync(categori.Id);
            if (oldCategori is null)
            {
                throw new Exception("محصولی با این شناسه پیدا نشد.");
            }
            oldCategori.Name = categori.Name;
            oldCategori.ImageFileName = categori.ImageFileName;
            _context.Categoris.Update(oldCategori);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Categori? categori = await _context.Categoris.FindAsync(id);
            if (categori is null)
            {
                throw new Exception("محصولی با این شناسه پیدا نشد.");
            }
            _context.Categoris.Remove(categori);
            await _context.SaveChangesAsync();
        }
    }
}
