using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace IbulakStoreServer.Data.Domain
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Categori> Categoris { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }
    }
}
