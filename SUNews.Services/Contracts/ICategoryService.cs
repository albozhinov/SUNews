using SUNews.Data.Models;

namespace SUNews.Services.Contracts
{
    public interface ICategoryService
    {
        Task<string> CreateCategory(string categoryName);

        Task<IEnumerable<Category>> GetAllCategories();
    }
}
