using Microsoft.AspNetCore.Mvc;

namespace SUNews.Web.Controllers
{
    public class Category : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
