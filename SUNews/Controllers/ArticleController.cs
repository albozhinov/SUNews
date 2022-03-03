namespace SUNews.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SUNews.Models;
    using SUNews.Services.Contracts;

    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService _articleService)
        {
            this.articleService = _articleService;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CreateArticle()
        {
            var model = new CreateArticleViewModel()
            {
                CategoriesList = new List<SelectListItem>
                {
                    new SelectListItem{ Text = "War"},
                    new SelectListItem{ Text = "Sport"},
                    new SelectListItem{ Text = "Covid-19"},
                }
            };            

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticle(CreateArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreateArticle");
            }

            await articleService.CreateArticle(model.Title, model.Content, model.ImageUrl, model.AuthorName, model.Categories);


            return Redirect("/Home/Index");
        }
    }
}
