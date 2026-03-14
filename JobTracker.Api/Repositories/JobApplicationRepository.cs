using Microsoft.EntityFrameworkCore;
using JobTracker.Api.Data;
using JobTracker.Api.Models;
using JobTracker.Api.Repositories.Interfaces;


namespace JobTracker.Api.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly AppDbContext _dbContext;
        public JobApplicationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(JobApplication jobApplication)
        {
            await _dbContext.JobApplications.AddAsync(jobApplication);
        }
        public async Task DeleteAsync(JobApplication jobApplication)
        {
            _dbContext.JobApplications.Remove(jobApplication);
           // await Task.CompletedTasBk;
        }
        public async Task<List<JobApplication>> GetAllByUserIdAsync(int userId)
        {
            return await _dbContext.JobApplications
                .Where(ja => ja.UserId == userId)
                .ToListAsync();
        }
        public async Task<JobApplication?> GetByIdAndUserIdAsync(int id, int userId)
        {
            return await _dbContext.JobApplications
                .FirstOrDefaultAsync(ja => ja.Id == id && ja.UserId == userId);
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    
    }
}
