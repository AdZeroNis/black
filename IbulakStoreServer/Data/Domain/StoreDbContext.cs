using IbulakStoreServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace IbulakStoreServer.Data.Domain
{
    public class StoreDbContext : IdentityDbContext<AppUser>
    { 
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string ADMIN_ROLE_ID = "a2a2df88-2952-408d-9c34-eca9177d92ac";
            const string ADMIN_ID = "2426167f-842e-4933-ae72-d8dfe34abf78";
            builder.Entity<IdentityRole>().HasData(
                                            new IdentityRole { Id = ADMIN_ROLE_ID, Name = "Admin", NormalizedName = "Admin".ToUpper() },
                                            new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() });

            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<AppUser>().HasData(

                new AppUser
                {
                    Id = ADMIN_ID,
                    UserName = "09119660028",
                    NormalizedUserName = "09119660028",
                    Email = "shaghayeghkrimi2923@gmail.com",
                    NormalizedEmail = "shaghayeghkrimi2923@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123#"),
                    SecurityStamp = string.Empty,
                    FullName = "شقایق کریمی"
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                });
        }
    }
}

