namespace SUNews.Services.Contracts
{
    using SUNews.Data.Models;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<Article> CreateArticleAsync(string title, string content, string imageUrl, string authorName, ICollection<string> categories);

        Task<ICollection<Article>> GetAllArticlesAsync();

        Task<Article> DetailsOfArticleAsync(Guid articleId);

        Task<Article> RateArticleAsync(Guid articleId, double rating);

        Task<Article> CommentArticle(Guid articleId, string comment);
    }
}
