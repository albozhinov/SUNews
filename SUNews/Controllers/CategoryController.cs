using Microsoft.AspNetCore.Mvc;

namespace SUNews.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateCategory()
        {
            return View();
        }
    }
}
