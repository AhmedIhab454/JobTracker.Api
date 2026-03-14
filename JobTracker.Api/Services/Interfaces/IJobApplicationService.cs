using JobTracker.Api.DTOs.JobApplication;

namespace JobTracker.Api.Services.Interfaces
{
    public interface IJobApplicationService
    {
        //GetAll
        //GetById
        //Create
        //Update
        //Delete

        Task<List<JobApplicationResponse>> GetAllAsync(int userId);
        Task<JobApplicationResponse?> GetByIdAsync(int id, int userId);
        Task<JobApplicationResponse> CreateAsync(int userId,CreateJobApplicationDto dto);
        Task<JobApplicationResponse?> UpdateAsync(int id,int userId,UpdateJobApplicationDto dto);
        Task<bool> DeleteAsync(int id,int userId);

    }
}
