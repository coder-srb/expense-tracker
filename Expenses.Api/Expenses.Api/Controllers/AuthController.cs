using Expenses.Api.Data;
using Expenses.Api.Dtos;
using Expenses.Api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Expenses.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AuthController(AppDbContext context, IPasswordHasher<User> passwordHasher) : ControllerBase
    {

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginUserDto payload)
        {
            var user = context.Users.FirstOrDefault(t => t.Email == payload.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email.");
            }

            var password = passwordHasher.VerifyHashedPassword(user, user.Password, payload.Password);
            if (password == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid password.");
            }
            
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] PostUserDto payload)
        {
            if(context.Users.Any(t => t.Email == payload.Email))
            {
                return BadRequest(payload.Email + " is already registered.");
            }

            var hashedPassword = passwordHasher.HashPassword(null, payload.Password);

            var newUser = new User
            {
                Email = payload.Email,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            return Ok(new { message = "Registered Successfully" });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-very-secure-secret-key-32-chars-long"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "srb.net",
                audience: "srb.net",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
