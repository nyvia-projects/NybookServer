using Microsoft.AspNetCore.Mvc;

namespace NybookApi.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
