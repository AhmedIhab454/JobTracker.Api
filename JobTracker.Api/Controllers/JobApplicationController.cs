using FluentValidation;
using JobTracker.Api.DTOs.JobApplication;
using JobTracker.Api.Services.Interfaces;
using JobTracker.Api.Validators.JobApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobApplicationController: ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;
        private readonly CreateJobApplicationDtoValidator _createValidator;
        private readonly UpdateJobApplicationDtoValidator _updateValidator;

        public JobApplicationController(
            IJobApplicationService jobApplicationService,
            CreateJobApplicationDtoValidator createValidator,
            UpdateJobApplicationDtoValidator updateValidator)
        {
            _jobApplicationService = jobApplicationService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _jobApplicationService.GetAllAsync(UserId);
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var application = await _jobApplicationService.GetByIdAsync(id, UserId);
            if (application == null) return NotFound("Job application not found.");
            return Ok(application);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateJobApplicationDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var created= await _jobApplicationService.CreateAsync(UserId, createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }   

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateJobApplicationDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var updated = await _jobApplicationService.UpdateAsync(id, UserId, updateDto);
            if (updated == null) return NotFound("Job application not found.");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _jobApplicationService.DeleteAsync(id, UserId);

            if (!deleted)
                return NotFound("Job application not found.");

            return NoContent();
        }
    }
}
