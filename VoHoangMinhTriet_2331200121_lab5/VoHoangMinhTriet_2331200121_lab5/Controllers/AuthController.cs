using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VoHoangMinhTriet_2331200121_lab5.Models;
using VoHoangMinhTriet_2331200121_lab5.Models.Context;
namespace VoHoangMinhTriet_2331200121_lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LibraryManagementContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(LibraryManagementContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult Login(Models.Request.LoginRequest request)
        {
            if (request == null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = _context.User.Include(u => u.UserRoles).ThenInclude(ur => ur.role).FirstOrDefault(u => u.Username == request.Username);
            if (user == null) return Unauthorized("nullUser");
            byte[] encoded;
            using (SHA256 sha256 = SHA256.Create())
            {
                encoded = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            }
            string hashString = BitConverter.ToString(encoded).Replace("-", "").ToLowerInvariant();
            if (hashString != user.Password) return Unauthorized("wrongPassword");
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        private string GenerateJwtToken (User user)
        {
            var roles = user.UserRoles.Select(ur => ur.role.RoleName).ToList();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("isActive", user.IsActive == true ? "1" : "0"),
                new Claim("JoinDate",user.CreatedDate.ToString("O")),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim("EmailVerified", user.IsActive && user.ActiveCode == null ? "True" : "False")
            };
            if (roles.Contains("Admin"))
            {
                claims.Add(new Claim("CanManageCategories", "True"));
            }
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            //foreach (var userRole in user.UserRoles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, userRole.role.RoleName));
            //}
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials:creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
