using FluentValidation;
using JobTracker.Api.DTOs.JobApplication;

namespace JobTracker.Api.Validators.JobApplication
{
    public class UpdateJobApplicationDtoValidator : AbstractValidator<UpdateJobApplicationDto>
    {
        public UpdateJobApplicationDtoValidator()
        {
            RuleFor(x => x.CompanyName)
               .NotEmpty().WithMessage("Company name is required.")
               .MaximumLength(100).WithMessage("Company name cannot exceed 100 characters.");

            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("Job title is required.")
                .MaximumLength(100).WithMessage("Job title cannot exceed 100 characters.");

            
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status value.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.")
                .When(x => x.Notes != null);
        }
    }
}
