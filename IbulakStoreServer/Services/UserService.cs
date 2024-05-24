using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Models.User;
using System.Xml.Linq;

namespace IbulakStoreServer.Services
{
    public class UserService
    {
        private readonly StoreDbContext _context;
        public UserService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<AppUser?> GetAsync(string id)
        {
            AppUser? user = await _context.Users.FindAsync(id);
            return user;
        }
      

        public async Task<List<AppUser>> GetsAsync()
        {
            List<AppUser> users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task AddAsync(UserAddRequestDto model)
        {
            AppUser user = new AppUser
            {
               
                FullName = model.FullName


            };
            _context.Entry(user).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(AppUser user)
        {
            var oldUser = await _context.Users.FindAsync(user.Id);

            if (oldUser == null)
            {
                throw new Exception("کاربری با این شناسه پیدا نشد.");
            }

          
            oldUser.FullName = user.FullName;

            _context.Entry(oldUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            AppUser? user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                throw new Exception("کاربری با این شناسه پیدا نشد.");
            }
            _context.Entry(user).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }

        internal async Task EditAsync(object user)
        {
            throw new NotImplementedException();
        }
    }
}