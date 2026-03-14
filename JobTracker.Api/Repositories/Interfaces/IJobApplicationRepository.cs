using JobTracker.Api.Models;

namespace JobTracker.Api.Repositories.Interfaces
{
    public interface IJobApplicationRepository
    {
        //getall
        //getById
        //Add
        //delete
        //savechanages
                Task<List<JobApplication>> GetAllByUserIdAsync(int UserId);
        Task<JobApplication?> GetByIdAndUserIdAsync(int id, int userId);
        Task AddAsync(JobApplication jobApplication);
        Task DeleteAsync(JobApplication jobApplication);
        Task SaveChangesAsync();

    }
}
