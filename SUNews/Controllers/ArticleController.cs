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
            this.userManager = _userManager;
        }


        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesAsync();

            return View(articles);
        }

        public IActionResult AllArticles()
        {
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> DetailsOfArticle(string id)
        {
            try
            {
                var article = await articleService.DetailsOfArticleAsync(id);
                var comment = new CreateCommentViewModel();

                return View(new ArticleAndCommentsViewModel() { DetailsOfArticlesServiceModel = article, CreateCommentViewModel = comment });
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));

            }

        }

        public async Task<IActionResult> LikeArticle(string id)
        {
            try
            {
                var loggedUser = await userManager.GetUserAsync(User);
                var article = await articleService.LikeArticleAsync(id, loggedUser.Id);

                return RedirectToAction("DetailsOfArticle", new { Id = id });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
