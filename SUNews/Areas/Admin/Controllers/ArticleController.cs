namespace SUNews.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SUNews.Areas.Admin.Models;
    using SUNews.Services.Contracts;
    using SUNews.Services.Models;

    [Area("Admin")]
    [Authorize(Roles = "Administrator, Owner, Manager")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;

        private static string StatusMessage { get; set; } = "";

        private static string MessageContent { get; set; } = "";

        public ArticleController(IArticleService _articleService, ICategoryService _categoryService)
        {
            this.articleService = _articleService;
            this.categoryService = _categoryService;
        }

        public async Task<IActionResult> AllArticles()
        {
            var articles = await articleService.GetAllArticlesAsync();

            ViewData[StatusMessage] = MessageContent;

            return View(articles);
        }
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

                StatusMessage = "SuccessMessage";
                MessageContent = "Article successfully deleted.";

                return RedirectToAction("Index" ,"Home");
            }
            catch (Exception)
            {
                StatusMessage = "ErrorMessage";
                MessageContent = "Sorry but something went wrong.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteArticle(string id)
        {
            try
            {
                await articleService.DeleteArticleAsync(id);
            }
            catch (Exception)
            {
                StatusMessage = "ErrorMessage";
                MessageContent = "Sorry but something went wrong.";

                return RedirectToAction("AllArticles", "Article");
            }

            StatusMessage = "SuccessMessage";
            MessageContent = "Article successfully deleted.";

            return RedirectToAction("AllArticles", "Article");
        }

        public async Task<IActionResult> EditArticle(string id)
        {

            EditArticleViewModel article = null;

            try
            {
                var dbArticle = await articleService.DetailsOfArticleAsync(id);
                var allCategories = await categoryService.GetAllCategoriesAsync();

                var selectListItems = allCategories
                                            .Select(c => new SelectListItem { Text = c.Name, })
                                            .ToList();


                article = new EditArticleViewModel()
                {
                    Id = dbArticle.Id.ToString(),
                    Title = dbArticle.Title,
                    Content = dbArticle.Content,
                    ImageUrl = dbArticle.ImageUrl,
                    AuthorName = dbArticle.AuthorName,
                    Categories = dbArticle.Categories.Select(a => a.Category.Name).ToList(),
                    CategoriesList = selectListItems
                };

            }
            catch (Exception)
            {
                StatusMessage = "ErrorMessage";
                MessageContent = "Sorry but something went wrong.";

                return RedirectToAction("AllArticles", "Article");
            }

            return View(article);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditArticle(EditArticleViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.CategoriesList = model.Categories.Select(c => new SelectListItem { Text = c }).ToList();
                return View(model);
            }

            try
            {
                await articleService.EditArticleAsync(model.Id,
                                                      model.Title,
                                                      model.Content,
                                                      model.ImageUrl,
                                                      model.AuthorName,
                                                      model.Categories);

                StatusMessage = "SuccessMessage";
                MessageContent = "Article successfully edited.";                
            }
            catch (Exception)
            {
                StatusMessage = "ErrorMessage";
                MessageContent = "Sorry but something went wrong.";

                return RedirectToAction("AllArticles", "Article");
            }

            return RedirectToAction("AllArticles", "Article");
        }
    }
}
