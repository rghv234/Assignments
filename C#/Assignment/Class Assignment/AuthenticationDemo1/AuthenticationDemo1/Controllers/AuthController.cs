﻿using AuthenticationDemo1.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration; 
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized(new { message = "Invalid username or password" });
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByNameAsync(registerModel.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new { message = "User already exists!" });
            }
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (registerModel.Role == "admin")
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);

                }

            }
            if (registerModel.Role == "user")
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }
                if (await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);

                }
            }
            return Ok(new Response
            {
                Status = "Success",
                StatusMessage = "User created successfully"
            });
        }
    }
}
