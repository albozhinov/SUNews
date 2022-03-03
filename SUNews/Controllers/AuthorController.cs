using Microsoft.AspNetCore.Mvc;

namespace SUNews.Web.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
