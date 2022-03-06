using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SUNews.Services.Contracts;

namespace SUNews.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(string categoryName)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //Here we have category name and can return to view!
            var categoryToAdd = await categoryService.CreateCategory(categoryName);

            return Redirect("/Article/CreateArticle");
        }


        public async Task<IActionResult> AllCategories()
        {
            var categories = await categoryService.GetAllCategories();


            return View(categories);
        }
    }
}
