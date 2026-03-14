using JobTracker.Api.Models;


namespace JobTracker.Api.DTOs.JobApplication
{
    public class JobApplicationResponse
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public DateTime DateAppllied { get; set; }
        public string Status { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
