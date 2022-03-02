using Microsoft.AspNetCore.Mvc;

namespace SUNews.Web.Controllers
{
    public class Author : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
