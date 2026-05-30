using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VoHoangMinhTriet_2331200121_lab5.Models;
using VoHoangMinhTriet_2331200121_lab5.Models.Context;
using VoHoangMinhTriet_2331200121_lab5.Models.Request;

namespace VoHoangMinhTriet_2331200121_lab5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly LibraryManagementContext _context;
        public LoanController(LibraryManagementContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loans>>> GetLoan([FromQuery] LoanQueryParameter parameter)
        {
            var query = _context.Loan.AsQueryable();
            if (parameter.UserId != null)
            {
                query = query.Where(l => l.UserId == parameter.UserId);
            }
            if (parameter.Status != null)
            {
                query = query.Where(l => l.Status == parameter.Status);
            }
            if (parameter.StartDate != null && parameter.EndDate != null)
            {
                query = query.Where(l => DateTime.Compare(l.LoanDate, (DateTime)parameter.StartDate) >= 0 && DateTime.Compare(l.LoanDate, (DateTime)parameter.EndDate) <= 0);
            }
            var loans = await query.ToListAsync();
            return Ok(loans);
        }
        [HttpGet("history")]
        [Authorize("MinimumMembership")]
        public async Task<ActionResult<IEnumerable<Loans>>> GetLoanHistory()
        {
            string? userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                return Unauthorized("user id not identified");
            }
            List<Loans> history = await _context.Loan.Where(l => l.UserId == userId).ToListAsync();
            return Ok(history);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Loans>> GetLoanById(int id)
        {
            var loan = await _context.Loan.FirstOrDefaultAsync(l => l.LoanId == id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }
        [HttpPost]
        public async Task<ActionResult> CreateLoan([FromQuery] LoanCreateRequest parameters)
        {
            var book = _context.Book.FirstOrDefault(b => b.BookId == parameters.BookId);
            if (book == null) return NotFound("not found book with such id");
            if (book.AvailableCopies <= 0) return Conflict("not enough books");
            Loans loan = new Loans()
            {
                BookId = parameters.BookId,
                UserId = parameters.UserId,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                Status = 0
            };
            _context.Loan.Add(loan);
            book.AvailableCopies--;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateLoan(int id)
        {
            var loan = _context.Loan.FirstOrDefault(l => l.LoanId == id);
            if (loan == null) return NotFound();
            loan.ReturnDate = DateTime.Now;
            loan.Status = 1;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

