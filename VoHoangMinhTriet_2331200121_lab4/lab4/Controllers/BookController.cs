using System.Net;
using lab4.Models;
using lab4.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab4.Models.Request;
using System.IO;

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

        [HttpGet("{id:int}/read")]
        public async Task<IActionResult> GetBookPdfPath(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found." });
            }
            if (string.IsNullOrWhiteSpace(book.Pdf))
            {
                return NotFound(new { message = "This book does not have a PDF available for online reading." });
            }
            return Ok(new { pdfUrl = book.Pdf });
        }

        [HttpGet("{id:int}/deleted")]
        public async Task<ActionResult<IEnumerable<Books>>> GetDeletedBooks()
        {
            var query = _context.Book.AsQueryable();
            query.Where(b => b.IsActive == false);
            var bookList = await query.ToListAsync();
            return Ok(bookList);
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

            string relativeImageFolder = Path.Combine("uploads", "images", "books");
            string relativePdfFolder = Path.Combine("uploads", "pdfs");

            string absoluteImageFolder = Path.Combine(_env.WebRootPath, "uploads", "images","books");
            string absolutePdfFolder = Path.Combine(_env.WebRootPath, "uploads", "pdfs");
            Directory.CreateDirectory(absoluteImageFolder);
            Directory.CreateDirectory(absolutePdfFolder);
            string imgFileName = Guid.NewGuid() + Path.GetExtension(request.CoverImage.FileName);
            string pdfFileName = Guid.NewGuid() + Path.GetExtension(request.PdfFile.FileName);

            string imgPhysicalPath = Path.Combine(absoluteImageFolder, imgFileName);
            string pdfPhysicalPath = Path.Combine(absolutePdfFolder, pdfFileName);

            string imgDatabasePath = "/" + Path.Combine(relativeImageFolder, imgFileName).Replace('\\', '/');
            string pdfDatabasePath = "/" + Path.Combine(relativePdfFolder, pdfFileName).Replace('\\', '/');

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
            await using (var fileStream = new FileStream(pdfPhysicalPath, FileMode.CreateNew))
            {
                try
                {
                    await request.PdfFile.CopyToAsync(fileStream);
                }
                catch (IOException)
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
                TotalCopies = request.TotalCopies,
                AvailableCopies = request.TotalCopies,
                Avatar = imgDatabasePath,
                Pdf = pdfDatabasePath
            };
            _context.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById),new {id = book.BookId},book);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Books>> UpdateBook (int id, [FromBody] Books newBook)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null) { return NotFound(); }
            _context.Entry(book).CurrentValues.SetValues(newBook);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}/restore")]
        public async Task<ActionResult> RestoreBook(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null) return NotFound($"not found book with id {id} to delete");
            if (!book.IsActive) return BadRequest($"book with id {id} is already active");
            book.IsActive = true;
            await _context.SaveChangesAsync();
            return NoContent();
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

        [HttpDelete("{id:int}/hard")]
        public async Task<ActionResult> HardDeleteBookById(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null) return NotFound();
            if (!string.IsNullOrWhiteSpace(book.Avatar))
            {
                string absolutePath = Path.Combine(_env.WebRootPath, book.Avatar.TrimStart('/'));
                try
                {
                    System.IO.File.Delete(absolutePath);
                } catch (IOException) {return StatusCode(500); }
            }
            if (!string.IsNullOrWhiteSpace(book.Pdf))
            {
                string absolutePath = Path.Combine(_env.WebRootPath, book.Pdf.TrimStart('/'));
                try
                {
                    System.IO.File.Delete(absolutePath);
                }
                catch (IOException) { return StatusCode(500); }
            }
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
    
}
