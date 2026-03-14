
using JobTracker.Api.Models;    

namespace JobTracker.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernsmeAsync(string username);
        Task<bool> UsernameExistsAsync(string username);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
