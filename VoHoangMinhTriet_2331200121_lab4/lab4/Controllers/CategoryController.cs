using lab4.Models;
using lab4.Models.Context;
using lab4.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly LibraryManagementContext _context;
        public CategoryController(LibraryManagementContext context)
        {
            _context = context;
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
            List<Categories> list = await query.ToList();
            return Ok(query.ToList());
        }
    }
}
