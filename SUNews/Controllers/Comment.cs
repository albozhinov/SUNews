using Microsoft.AspNetCore.Mvc;

namespace SUNews.Web.Controllers
{
    public class Comment : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
