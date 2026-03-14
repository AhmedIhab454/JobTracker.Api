using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobTracker.Api.Models
{
    public enum ApplicationStatus
        {
            Applied,
            Interview,
            Offer,
            Rejected
        }
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        public string JobTitle { get; set; } = string.Empty;
        [Required]
        public DateTime DateApplied { get; set; } = DateTime.UtcNow;
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
        public string? Notes { get; set; }
        [Required]
        public int UserId { get; set; } 
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
