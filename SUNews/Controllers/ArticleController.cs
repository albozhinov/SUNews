namespace SUNews.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SUNews.Data.Models;
    using SUNews.Models;
    using SUNews.Providers;
    using SUNews.Services.Constants;
    using SUNews.Services.Contracts;
    using SUNews.Services.Models;

    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;
        private readonly IUserManager<User> userManager;

        public ArticleController(IArticleService _articleService, 
                                 ICategoryService _categoryService,
                                 IUserManager<User> _userManager)
        {
            this.articleService = _articleService;
            this.categoryService = _categoryService;
            this.userManager = _userManager;
        }


        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesAsync();           

            return View(articles);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateArticle()
        {
            var allCategories = await categoryService.GetAllCategoriesAsync();

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

                return Redirect("/Article/Index");
            }
            catch (Exception ex)
            {

                ViewData[MessageConstant.ErrorMessage] = ex.Message;

                return View("Error");
            }
        }

        public  IActionResult AllArticles()
        {
            return RedirectToAction("Index", "Article");
        }

        [Authorize]
        public async Task<IActionResult> DetailsOfArticle(string id)
        {
            var article = await articleService.DetailsOfArticleAsync(id);
            var comment = new CreateCommentViewModel();

            return View(new ArticleAndCommentsViewModel() { DetailsOfArticlesServiceModel = article, CreateCommentViewModel = comment });
        }

        public async Task<IActionResult> LikeArticle(string id)
        {
            var loggedUser = await userManager.GetUserAsync(User);
            var article = await articleService.LikeArticleAsync(id, loggedUser.Id);

            return RedirectToAction("DetailsOfArticle", new { Id = id });
        }
    }
}
