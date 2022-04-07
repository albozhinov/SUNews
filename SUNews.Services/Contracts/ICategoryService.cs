using SUNews.Data.Models;
using SUNews.Services.Models;

namespace SUNews.Services.Contracts
{
    public interface ICategoryService
    {
        Task<string> CreateCategoryAsync(string categoryName);

        Task<IEnumerable<CategoryServiceModel>> GetAllCategoriesAsync();
    }
}
