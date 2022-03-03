using Microsoft.AspNetCore.Mvc;

namespace SUNews.Web.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
