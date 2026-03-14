using Microsoft.EntityFrameworkCore;
using JobTracker.Api.Data;
using JobTracker.Api.Models;
using JobTracker.Api.Repositories.Interfaces;

namespace JobTracker.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
           await _dbContext.Users.AddAsync(user);
        }

        public async Task<User?> GetByUsernsmeAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
             return await _dbContext.Users.AnyAsync(u => u.Username == username);
        }
    }
}
