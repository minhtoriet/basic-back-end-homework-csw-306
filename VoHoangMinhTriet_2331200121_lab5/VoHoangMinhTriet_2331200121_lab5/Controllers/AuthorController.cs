using VoHoangMinhTriet_2331200121_lab5.Models;
using VoHoangMinhTriet_2331200121_lab5.Models.Context;
using VoHoangMinhTriet_2331200121_lab5.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VoHoangMinhTriet_2331200121_lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly LibraryManagementContext _context;
        private readonly IWebHostEnvironment _env;
        public AuthorController (LibraryManagementContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authors>>> GetAuthors(string? name)
        {
            var query = _context.Author.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = _context.Author.Where(a => string.Compare(a.FirstName, name) == 0 || string.Compare(a.LastName, name) == 0);
            }
            var authors = await query.ToListAsync();
            return Ok(authors);
        }
        [HttpGet("{id:int}")]
        public ActionResult<Authors> GetAuthorById(int id)
        {
            var author = _context.Author.FirstOrDefault(a => a.AuthorId == id);
            if (author == null) return NotFound();
            else return Ok(author);
        }
        [HttpPost]
        public async Task<ActionResult<Authors>> CreateAuthor([FromForm] AuthorCreateRequest request)
        {
            string? imgDatabasePath = null;
            if (request.CoverImage != null)
            {
                string relativeImageFolder = Path.Combine("uploads", "images", "authors");

                string absoluteImageFolder = Path.Combine(_env.WebRootPath, "uploads", "images", "authors");

                Directory.CreateDirectory(absoluteImageFolder);

                string imgFileName = Guid.NewGuid() + Path.GetExtension(request.CoverImage.FileName);

                string imgPhysicalPath = Path.Combine(absoluteImageFolder, imgFileName);
                imgDatabasePath = "/" + Path.Combine(relativeImageFolder, imgFileName).Replace("\\","/");
                await using (var fileStream = new FileStream(imgPhysicalPath, FileMode.CreateNew))
                {
                    try
                    {
                        await request.CoverImage.CopyToAsync(fileStream);
                    }
                    catch (IOException)
                    {
                        return StatusCode(500);
                    }
                }
            }
            var author = new Authors
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedDate = DateTime.Now,
                IsActive = true,
                Avatar = imgDatabasePath
            };
            _context.Add(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.AuthorId }, new AuthorResponseDto
            {
                Id = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                AvatarUrl = author.Avatar
            });
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] Authors newAuthor)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null) { return NotFound(); }
            _context.Entry(author).CurrentValues.SetValues(newAuthor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Author.FirstOrDefaultAsync(a => a.AuthorId == id);
            if (author == null) return NotFound();
            author.IsActive = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
