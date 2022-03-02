namespace SUNews.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class Article : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CreateArticle()
        {
            return View();
        }
    }
}
