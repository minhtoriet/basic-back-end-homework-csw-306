using lab4.Models;
using lab4.Models.Context;
using lab4.Models.Request;
using lab4.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;

namespace lab4.Controllers
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
        [HttpPost]
        [Route("api/[controller]/register")]
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
                ActiveCode = ActivationCodeGenerator.GenerateCode()

            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("{code}")]
        [Route("api/[controller]/activate")] 
        public async Task<ActionResult> ActivateUser([FromQuery] UserActivateRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.ActivateCode)) return BadRequest("activation code cant be null or empty");
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
