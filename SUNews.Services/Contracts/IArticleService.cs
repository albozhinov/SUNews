namespace SUNews.Services.Contracts
{
    using SUNews.Data.Models;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<Article> CreateArticle();
    }
}
