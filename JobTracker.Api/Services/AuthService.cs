using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JobTracker.Api.DTOs.Auth;
using JobTracker.Api.Models;
using JobTracker.Api.Repositories.Interfaces;
using JobTracker.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace JobTracker.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepository userRepository,IConfiguration configuration)
        {
            _userRepository= userRepository;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {

            bool usernameExists = await _userRepository.UsernameExistsAsync(registerDto.Username);
            if(usernameExists)
                {
                throw new Exception("Username already exists");
            }
            string passwordHash = HashPassword(registerDto.Password);
            var user = new User
            {
                Username = registerDto.Username,    
                PasswordHash = passwordHash
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return "User registered successfully.";

        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernsmeAsync(loginDto.Username);
            if (user == null||user.PasswordHash!= HashPassword(loginDto.Password))
            {
                throw new UnauthorizedAccessException("Invalid username ir password");
            }
            return GenerateJwtToken(user);
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedBytes);
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration["JwtSettings:ExpiryMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
