using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoHoangMinhTriet_2331200121_lab5.Models.Context;

namespace VoHoangMinhTriet_2331200121_lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly LibraryManagementContext _context;
        public AdminController (LibraryManagementContext context)
        {
            _context = context;
        }
        [HttpGet("dashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return Ok("Welcome admin");
        }
    }
}
