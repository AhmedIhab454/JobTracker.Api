using JobTracker.Api.DTOs.JobApplication;
using JobTracker.Api.Models;
using JobTracker.Api.Repositories.Interfaces;
using JobTracker.Api.Services.Interfaces;

namespace JobTracker.Api.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        public JobApplicationService(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<JobApplicationResponse> CreateAsync(int userId, CreateJobApplicationDto dto)
        {
            var application = new JobApplication
            {
                UserId = userId,
                CompanyName = dto.CompanyName,
                JobTitle = dto.JobTitle,
                DateApplied = DateTime.UtcNow,
                Status = ApplicationStatus.Applied,
                Notes = dto.Notes
            };
            await _jobApplicationRepository.AddAsync(application);
            await _jobApplicationRepository.SaveChangesAsync();
            return MapToResponse(application);

        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var application = await _jobApplicationRepository
                .GetByIdAndUserIdAsync(id, userId);
            if (application == null) return false;
            await _jobApplicationRepository.DeleteAsync(application);
            await _jobApplicationRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<JobApplicationResponse>> GetAllAsync(int userId)
        {
            var applications =await  _jobApplicationRepository.GetAllByUserIdAsync(userId);
            return applications.Select(a=>MapToResponse(a)).ToList();

        }

        public async Task<JobApplicationResponse?> GetByIdAsync(int id, int userId)
        {
            var application = await _jobApplicationRepository
                .GetByIdAndUserIdAsync(id, userId);
            if (application == null)
                return null;
            return MapToResponse(application);
        }

        public async Task<JobApplicationResponse?> UpdateAsync(int id, int userId, UpdateJobApplicationDto dto)
        {
            var application = await _jobApplicationRepository
                .GetByIdAndUserIdAsync(id, userId);
            if (application == null) return null;
            application.CompanyName = dto.CompanyName;
            application.JobTitle = dto.JobTitle;
            application.Status = dto.Status;
            application.Notes = dto.Notes;
            await _jobApplicationRepository.SaveChangesAsync();
            return MapToResponse(application);  
        }

            private JobApplicationResponse MapToResponse(JobApplication application)
            {
                return new JobApplicationResponse
                {
                    Id = application.Id,
                    CompanyName = application.CompanyName,
                    JobTitle = application.JobTitle,
                    DateAppllied= application.DateApplied,
                    
                    Status = application.Status.ToString(),
                    Notes = application.Notes
                };
        }
    }
}
