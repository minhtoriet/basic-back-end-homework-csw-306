using lab4.Models.Context;
using lab4.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab4.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ReportController : ControllerBase
{
    private readonly LibraryManagementContext _context;
    
    public ReportController(LibraryManagementContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MostBorrowedBooksDto>>> GetMostBorrowedBooks([FromQuery] MostBorrowedBooksParameter parameter)
    {
        List<MostBorrowedBooksDto> topBooks = await _context.Loan.Where(l => l.LoanDate >= parameter.FromDate && l.LoanDate <= parameter.ToDate)
                                                                  .GroupBy(l => new { l.BookId, l.Book.Title }).Select(g => new MostBorrowedBooksDto
                                                                  {
                                                                      BookId = g.Key.BookId,
                                                                      Title = g.Key.Title,
                                                                      BorrowCount = g.Count()
                                                                  }).OrderByDescending(b => b.BorrowCount).Take(parameter.Top).ToListAsync();
        return Ok(topBooks);
    }

    
}