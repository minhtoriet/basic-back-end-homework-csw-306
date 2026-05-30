using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoHoangMinhTriet_2331200121_lab5.Models;
using VoHoangMinhTriet_2331200121_lab5.Models.Context;
using VoHoangMinhTriet_2331200121_lab5.Models.Request;

namespace VoHoangMinhTriet_2331200121_lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly LibraryManagementContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoryController(LibraryManagementContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategoryById(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetBooks([FromQuery] CategoryQueryParameter parameter)
        {
            var query = _context.Category.AsQueryable();
            query = query.Where(c => c.IsActive == true);
            if (!string.IsNullOrWhiteSpace(parameter.Name))
            {
                query = query.Where(c => c.Name == parameter.Name);
            }
            List<Categories> list = await query.ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        [Authorize("CanManageCategories")]
        public async Task<ActionResult<Categories>> AddCategory([FromBody] CategoryCreateRequest request)
        {
            string? imgDatabasePath = null;
            if (request.Avatar != null)
            {
                string relativeImgFolder = Path.Combine("uploads", "images", "categories");
                string absoluteImgFolder = Path.Combine(_env.WebRootPath, "uploads", "images", "categories");
                Directory.CreateDirectory(absoluteImgFolder);
                string imgFileName = Guid.NewGuid() + Path.GetExtension(request.Avatar.FileName);
                string imgPhysicalPath = Path.Combine(absoluteImgFolder, imgFileName);
                imgDatabasePath = "/" + Path.Combine(relativeImgFolder, imgFileName).Replace("\\", "/");
                await using (var fileStream = new FileStream(imgPhysicalPath, FileMode.CreateNew))
                {
                    try
                    {
                        await request.Avatar.CopyToAsync(fileStream);
                    }
                    catch (IOException)
                    {
                        return StatusCode(500);
                    }
                }
            }
            var category = new Categories()
            {
                Name = request.Name,
                Description = request.Description,
                Avatar = imgDatabasePath,
                CreatedDate = DateTime.Now,
                IsActive = true
            };
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        [Authorize("CanManageCategories")]
        public async Task<ActionResult> EditCategory(int id, [FromForm] CategoryUpdateRequest request)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null) return NotFound();
            category.Name = request.Name;
            category.Description = request.Description;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}/toggle")]
        [Authorize("CanManageCategories")]
        public async Task<ActionResult> ToggleCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null) return NotFound();
            category.IsActive = !category.IsActive;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
