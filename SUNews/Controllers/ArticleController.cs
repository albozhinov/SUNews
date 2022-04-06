namespace SUNews.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SUNews.Models;
    using SUNews.Services.Constants;
    using SUNews.Services.Contracts;
    using SUNews.Services.Models;

    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;

        public ArticleController(IArticleService _articleService, ICategoryService _categoryService)
        {
            this.articleService = _articleService;
            this.categoryService = _categoryService;
        }


        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesAsync();

            return View(articles);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateArticle()
        {
            var allCategories = await categoryService.GetAllCategories();

            var selectListItems = allCategories
                                    .Select(c => new SelectListItem { Text = c.Name, })
                                    .ToList();

            var model = new CreateArticleViewModel()
            {
                CategoriesList = selectListItems
            };

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateArticle(CreateArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ////Here we can return View with invalid data value.
                ///This we be helpful to write correct value and better user experience.
                model.CategoriesList = model.Categories.Select(c => new SelectListItem { Text = c }).ToList();
                return View(model);
            }

            try
            {
                await articleService.CreateArticleAsync(model.Title,
                                                        model.Content,
                                                        model.ImageUrl,
                                                        model.AuthorName,
                                                        model.Categories);

                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {

                ViewData[MessageConstant.ErrorMessage] = ex.Message;

                return View("Error");
            }
        }

        public async Task<IActionResult> AllArticles()
        {
            var allArticles = await articleService.GetAllArticlesAsync();


            return RedirectToAction("Index", "Home", allArticles);
        }

        public async Task<IActionResult> DetailsOfArticle(string id)
        {
            var article = await articleService.DetailsOfArticleAsync(id);            

            return View(article);
        }
    }
}
