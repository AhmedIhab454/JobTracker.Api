using FluentValidation;
using JobTracker.Api.DTOs.Auth;
using JobTracker.Api.Services.Interfaces;
using JobTracker.Api.Validators.Auth;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Api.Controllers
{
[Route("api/[controller]")]
        [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly RegisterDtoValidator _registerValidator;
        private readonly LoginDtoValidator _loginValidator;
        public AuthController(IAuthService authService
            , RegisterDtoValidator registerValidator,
            LoginDtoValidator loginValidator
            )
        {
            _authService = authService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var validationResult = await _registerValidator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e=>e.ErrorMessage));
            }
            var message = await _authService.RegisterAsync(registerDto);
            return Ok(message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var token = await _authService.LoginAsync(loginDto);
            return Ok(new { token });

        }
    }
}
