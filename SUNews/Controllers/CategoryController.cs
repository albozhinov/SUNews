namespace SUNews.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SUNews.Models;
    using SUNews.Services.Contracts;
    using SUNews.Services.Models;

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.AllCategories();


            var model = new CategoriesAndArticlesViewModel()
            {
                Categories = categories
            };

            return View(model);
        }

        public async Task<IActionResult> Category(string id)
        {   
            try
            {
                var allArticlesByCategory = await categoryService.GetArticlesByCategory(id);

                var model = allArticlesByCategory;

                return PartialView("_CategoryArticleGrid", model);
            }
            catch (Exception)
            {

                return NotFound();
            }
            
        }
    }
}
