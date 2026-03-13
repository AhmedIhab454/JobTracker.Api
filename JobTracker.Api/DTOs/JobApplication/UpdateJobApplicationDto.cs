using JobTracker.Api.Models;

namespace JobTracker.Api.DTOs.JobApplication
{
    public class UpdateJobApplicationDto
    {
        public string CompanyName { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public ApplicationStatus Status { get; set; }
        public string? Notes { get; set; }
    }
}
