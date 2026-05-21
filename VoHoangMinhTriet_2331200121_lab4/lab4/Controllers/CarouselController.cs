using lab4.Models;
using lab4.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq.Expressions;

namespace lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarouselController : ControllerBase
    {
        private readonly LibraryManagementContext _context;
        private readonly IWebHostEnvironment _env;
        public CarouselController(LibraryManagementContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Carousels>> GetCarousel()
        {
            var carousels = _context.Carousel.ToList();
            return Ok(carousels);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Carousels>> GetCarouselById(int id)
        {
            var carousel = await _context.Carousel.FirstOrDefaultAsync(u => u.CarouselId == id);
            if (carousel == null) return NotFound($"Item with ID {id} was not found.");
            return Ok(carousel);
        }
        [HttpPost]
        public async Task<ActionResult<Carousels>> AddCarousel (Carousels c)
        {
            var carousel = await _context.Carousel.FirstOrDefaultAsync(u => u.CarouselId == c.CarouselId);
            if (carousel != null) return BadRequest();
            _context.Carousel.Add(c);
            await _context.SaveChangesAsync();
            return c;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Carousels>> EditCarousel (int id, [FromBody] Carousels c)
        {
            var carousel = await _context.Carousel.FirstOrDefaultAsync(u => u.CarouselId == id);
            if (carousel == null) return NotFound($"Item with ID {id} was not found.");
            _context.Entry(carousel).CurrentValues.SetValues(c);
            await _context.SaveChangesAsync();
            return c;
        }
        [HttpDelete("{id}")] 
        public async Task<ActionResult> DeteleCarousel (int id)
        {
            var carousel = await _context.Carousel.FirstOrDefaultAsync(u => u.CarouselId == id);
            if (carousel == null) return NotFound($"Item with ID {id} was not found.");
            if (!string.IsNullOrEmpty(carousel.ImageUrl))
            {
                try
                {
                    string relativePath = carousel.ImageUrl.TrimStart('/');
                    string physicalPath = Path.Combine(_env.WebRootPath, relativePath);
                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                } catch (IOException ioe)
                {
                    return StatusCode(500, "Internal Server Error");
                }

            }
            _context.Carousel.Remove(carousel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
