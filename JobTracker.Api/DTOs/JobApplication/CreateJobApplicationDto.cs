namespace JobTracker.Api.DTOs.JobApplication
{
    public class CreateJobApplicationDto
    {
        public string CompanyName { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public string? Notes { get; set; }

        //no status or date applied because those will be set by the backend when the application is created
    }
}
