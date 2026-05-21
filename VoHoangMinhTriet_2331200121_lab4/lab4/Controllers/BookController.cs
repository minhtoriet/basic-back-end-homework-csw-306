using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
