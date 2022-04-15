namespace SUNews.Services.Contracts
{
    using SUNews.Services.Models;

    public interface ICategoryService
    {
        Task<string> CreateCategoryAsync(string categoryName);

        Task<IEnumerable<CategoryServiceModel>> GetAllCategoriesAsync();

        Task<IEnumerable<ArticleServiceModel>> GetArticlesByCategory(string id);

        Task<IEnumerable<CategoryServiceModel>> AllCategories();
    }
}
