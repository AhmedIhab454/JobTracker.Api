using Microsoft.EntityFrameworkCore;
using JobTracker.Api.Models;


namespace JobTracker.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<JobApplication> JobApplications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.JobApplications)
            //    .WithOne(ja => ja.User)
            //    .HasForeignKey(ja => ja.UserId);
            modelBuilder.Entity<JobApplication>()
                .Property(J=> J.Status)
                .HasConversion<string>();

            modelBuilder.Entity<JobApplication>()
                .HasOne(J=> J.User)
                .WithMany(u => u.JobApplications)
                .HasForeignKey(ja => ja.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
