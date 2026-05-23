using System.Net;
using lab4.Models;
using lab4.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab4.Models.Request;


namespace lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly LibraryManagementContext _context;
        private readonly IWebHostEnvironment _env;
        
        public BookController (LibraryManagementContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetBooks([FromQuery] BookQueryParameters parameters)
        {
            var query = _context.Book.AsQueryable();
            if (!string.IsNullOrWhiteSpace(parameters.Title))
            {
                query = query.Where(b => b.Title.Contains(parameters.Title));
            }
            if (parameters.CategoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == parameters.CategoryId.Value);
            }

            if (parameters.AuthorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == parameters.AuthorId.Value);
            }
            int itemsToSkip = (parameters.PageNumber - 1) * parameters.PageSize;
            query = query.Skip(itemsToSkip).Take(parameters.PageSize);
            var books = await query.ToListAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBookById(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Books>> CreateBook([FromForm] BookCreateRequest request)
        {
            if (request.CoverImage == null || request.CoverImage.Length == 0)
            {
                return BadRequest("where cover file you fuck?");
            }

            if (request.PdfFile == null || request.PdfFile.Length == 0)
            {
                return BadRequest("where pdf file you fuck?");
            }

            string imageFolder = Path.Combine(_env.WebRootPath, "uploads", "images");
            string pdfFolder = Path.Combine(_env.WebRootPath, "uploads", "pdfs");
            Directory.CreateDirectory(imageFolder);
            Directory.CreateDirectory(pdfFolder);
            string imgFileName = Guid.NewGuid() + Path.GetExtension(request.CoverImage.FileName);
            string pdfFileName = Guid.NewGuid() + Path.GetExtension(request.PdfFile.FileName);

            string imgPhysicalPath = Path.Combine(imageFolder, imgFileName);
            string pdfPhysicalPath = Path.Combine(pdfFolder, pdfFileName);

            await using (var fileStream = new FileStream(imgPhysicalPath, FileMode.CreateNew))
            {
                try
                {
                    await request.CoverImage.CopyToAsync(fileStream);
                }
                catch (IOException ioe)
                {
                    return StatusCode(500);
                }
                
            }
            await using (var fileStream = new FileStream(pdfPhysicalPath, FileMode.CreateNew))
            {
                try
                {
                    await request.PdfFile.CopyToAsync(fileStream);
                }
                catch (IOException ioe)
                {
                    return StatusCode(500);
                }
            }

            var book = new Books()
            {
                Title = request.Title,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Publisher = request.Publisher,
                PublishedYear = request.PublishedYear,
                IsActive = true,
                AuthorId = request.AuthorId,
                CreatedDate = DateTime.Now,
                Avatar = imgPhysicalPath,
                Pdf = pdfPhysicalPath
            };
            _context.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById),new {title = book.Title},book);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBookById(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null) return NotFound();
            book.IsActive = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
