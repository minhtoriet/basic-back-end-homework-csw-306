using lab4.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers;

public class ReportController : ControllerBase
{
    private readonly LibraryManagementContext _context;
    
    public ReportController(LibraryManagementContext context)
    {
        _context = context;
    }
    [HttpGet]
    
    
}