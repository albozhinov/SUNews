namespace SUNews.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SUNews.Models;
    using SUNews.Services.Constants;
    using SUNews.Services.Contracts;

    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;

        public ArticleController(IArticleService _articleService, ICategoryService _categoryService)
        {
            this.articleService = _articleService;
            this.categoryService = _categoryService;
        }


        public IActionResult Index()
        {
            return View();
        }


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

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticle(CreateArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreateArticle");
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

                return View();
            }
        }
    }
}
