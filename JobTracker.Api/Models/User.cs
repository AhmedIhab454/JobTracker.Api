using System.ComponentModel.DataAnnotations;

namespace JobTracker.Api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public List<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    }
}
