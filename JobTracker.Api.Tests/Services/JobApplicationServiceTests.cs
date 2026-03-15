using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using JobTracker.Api.DTOs.JobApplication;
using JobTracker.Api.Models;
using JobTracker.Api.Repositories.Interfaces;
using JobTracker.Api.Services;


namespace JobTracker.Api.Tests.Services
{
    public class JobApplicationServiceTests
    {

        private readonly Mock<IJobApplicationRepository> _mockRepository;
        private readonly JobApplicationService _service;
        public JobApplicationServiceTests()
        {
            _mockRepository = new Mock<IJobApplicationRepository>();
            _service = new JobApplicationService(_mockRepository.Object);
        }

        //naming convention: MethodName_Scenario_ExpectedResult

        [Fact]
        public async Task GetAllAsync_WithExistingApplications_ReturnsAllApplications()
        {
            // Arrange
            int userId = 1;
            var applications = new List<JobApplication>
            {

                 new JobApplication
                {
                    Id = 1,
                    CompanyName = "Google",
                    JobTitle = "Backend Developer",
                    Status = ApplicationStatus.Applied,
                    DateApplied = DateTime.UtcNow,
                    UserId = userId
                },
                new JobApplication
                {
                    Id = 2,
                    CompanyName = "Microsoft",
                    JobTitle = ".NET Developer",
                    Status = ApplicationStatus.Interview,
                    DateApplied = DateTime.UtcNow,
                    UserId = userId
                }
            };
            _mockRepository
                .Setup(r=>r.GetAllByUserIdAsync(userId))
                .ReturnsAsync(applications);

            // Act
            var result = await _service.GetAllAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].CompanyName.Should().Be("Google");
            result[1].CompanyName.Should().Be("Microsoft");


        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsCorrectApplication()
        {
            //Arrange
            int userId = 1;
            int applicationId = 1;
            var fakeApplication = new JobApplication
            {
                Id = applicationId,
                CompanyName = "Google",
                JobTitle = "Backend Developer",
                Status = ApplicationStatus.Applied,
                DateApplied = DateTime.UtcNow,
                UserId = userId
            };

            _mockRepository
                .Setup(r=>r.GetByIdAndUserIdAsync(applicationId,userId))
                .ReturnsAsync(fakeApplication);
            //Act
            var result = await _service.GetByIdAsync(applicationId, userId);

            //Assert
            result.Should().NotBeNull();
            result!.CompanyName.Should().Be("Google"); 
            result.JobTitle.Should().Be("Backend Developer");
            result.Status.Should().Be("Applied");
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_RetuensNull()
        { 
        //Arrange
        _mockRepository
                .Setup(r=>r.GetByIdAndUserIdAsync(999,1))
                .ReturnsAsync((JobApplication?)null);

            //Act
            var result = await _service.GetByIdAsync(999, 1);
            //Assert
                        result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_WithValidData_ReturnsCreatedApplication()
        {

            //Arrange
            int userId = 1;
            var createDto = new CreateJobApplicationDto
            {
                CompanyName = "Amazon",
                JobTitle = "Senior Developer",
                Notes = "Applied via LinkedIn"
            };

            _mockRepository
                .Setup(r => r.AddAsync(It.IsAny<JobApplication>()))
                .Returns(Task.CompletedTask);

            _mockRepository
                .Setup(r=>r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            //Act
            var result = await _service.CreateAsync(userId,createDto);

            //Asset
            result.Should().NotBeNull();
            result.CompanyName.Should().Be("Amazon");
            result.JobTitle.Should().Be("Senior Developer");
            result.Status.Should().Be("Applied");
        }

        [Fact]
        public async Task DeletAsync_WithInvalidId_ReturnsFalse()
        {
            //Arrange
            _mockRepository
                .Setup(r=>r.GetByIdAndUserIdAsync(999,1))
                .ReturnsAsync((JobApplication?)null);
            //Act
            var result = await _service.DeleteAsync(999, 1);
            //Assert
            result.Should().BeFalse();


        }

    }
}
