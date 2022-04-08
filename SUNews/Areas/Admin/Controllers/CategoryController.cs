namespace SUNews.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SUNews.Areas.Admin.Models;
    using SUNews.Services.Contracts;

    [Area("Admin")]
    [Authorize(Roles = "Administrator, Owner, Manager")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            this.categoryService = _categoryService;
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await categoryService.CreateCategoryAsync(model.CategoryName);
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "Sorry but something went wrong.";

                return View();
            }

            ViewData["SuccessMessage"] = "Category successfully created.";
            return View();
        }

        public async Task<IActionResult> AllCategories()
        {
            var categories = await categoryService.GetAllCategoriesAsync();


            return View(categories);
        }
    }
}
