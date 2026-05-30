using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VoHoangMinhTriet_2331200121_lab5.Models;
using VoHoangMinhTriet_2331200121_lab5.Models.Context;
using VoHoangMinhTriet_2331200121_lab5.Models.Request;
using VoHoangMinhTriet_2331200121_lab5.Utilities;

namespace VoHoangMinhTriet_2331200121_lab5.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly LibraryManagementContext _context;
        public UserController(LibraryManagementContext context)
        {
            _context = context;
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMe()
        {
            var username = User.Identity?.Name;
            var roles = User.FindAll(ClaimTypes.Role)?.Select(r => r.Value).ToList();
            return Ok(new { username, roles });
        }

        [HttpPost("register")]
        public async Task<ActionResult> UserRegister([FromForm] UserCreateRequest request)
        {
            byte[] encoded;
            using (SHA256 sha256 = SHA256.Create())
            {
                encoded = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            }
            string hashString = BitConverter.ToString(encoded).Replace("-", "").ToLowerInvariant();

            var user = new User()
            {
                FullName = request.FullName,
                Password = hashString,
                Email = request.EmailAddress,
                Status = 0,
                CreatedDate = DateTime.Now,
                IsLocked = false,
                IsDeleted = false,
                IsActive = false,
                ActiveCode = ActivationCodeGenerator.GenerateCode(),
                Username = request.Username

            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("active-user")]
        [Authorize(Policy = "ActiveUserOnly")]
        public async Task<ActionResult<IEnumerable<User>>> GetActiveUsers()
        {
            var users = await _context.User.Where(u => u.IsActive == true).ToListAsync();
            return Ok(users);
        }

        [HttpGet("activate")]
        public async Task<ActionResult> ActivateUser([FromQuery] UserActivateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ActivateCode)) return BadRequest("activation code cant be null or empty");
            var user = _context.User.FirstOrDefault(u => u.ActiveCode == request.ActivateCode);
            if (user == null) return NotFound("invalid/expired code");
            if (user.IsActive) return BadRequest("user already activated");
            user.IsActive = true;
            user.ActiveCode = null;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
