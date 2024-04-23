using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IbulakStoreServer.Services
{
    public class UserService
    {
        private readonly StoreDbContext _context;
        public UserService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetAsync(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            return user;
        }
        public async Task<List<User>> GetsAsync()
        {
            List<User> users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(User user)
        {
            User? oldUser = await _context.Users.FindAsync(user.Id);
            if (oldUser is null)
            {
                throw new Exception("کاربری با این شناسه پیدا نشد.");
            }
            oldUser.Name = user.Name;
            oldUser.LastName = user.LastName;
            _context.Users.Update(oldUser);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                throw new Exception("کاربری با این شناسه پیدا نشد.");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        internal async Task EditAsync(object user)
        {
            throw new NotImplementedException();
        }
    }
}