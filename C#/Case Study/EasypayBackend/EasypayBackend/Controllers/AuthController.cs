using Asp.Versioning;
using EasypayBackend.Dtos;
using EasypayBackend.Models;
using EasypayBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasypayBackend.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public AuthController(IUserRepository userRepository, IConfiguration configuration, ILogger<AuthController> logger, IEmployeeRepository employeeRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                _logger.LogWarning("Failed login attempt for email: {Email}", loginDto.Email);
                return BadRequest(new { Message = "Invalid credentials" });
            }

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token, User = new { user.Id, user.Email, user.Role } });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterWithEmployeeDto registerDto)
        {
            var users = await _userRepository.FindAsync(u => u.Email == registerDto.Email);
            if (users.Any())
            {
                _logger.LogWarning("Registration failed: Email {Email} already exists", registerDto.Email);
                return BadRequest(new { Message = "User already exists" });
            }

            var user = new User
            {
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = registerDto.Role,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync(); // 🛠 Save now so user.Id is populated

            if (registerDto.Role == "Employee" || registerDto.Role == "PayrollProcessor" || registerDto.Role == "Manager")
            {
                var employee = new Employee
                {
                    UserId = user.Id, // ✅ Now this has the actual DB-generated value
                    Name = registerDto.Name ?? $"{registerDto.Email.Split('@')[0]}",
                    ContactInfo = registerDto.Email,
                    TaxWithholding = "{}",
                    Salary = 0,
                    LeaveBalance = 20
                };
                await _employeeRepository.AddAsync(employee);
                await _employeeRepository.SaveChangesAsync(); // 🛠 Save employee separately
            }

            return Ok(new { Id = user.Id, Email = user.Email, Role = user.Role });

        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}