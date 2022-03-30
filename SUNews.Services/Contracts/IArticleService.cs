﻿namespace SUNews.Services.Contracts
{
    using SUNews.Services.Models;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<DetailsOfArticlesServiceModel> CreateArticleAsync(string title, string content, string imageUrl, string authorName, ICollection<string> categories);

        Task<ICollection<AllArticlesServiceModel>> GetAllArticlesAsync();

        Task<DetailsOfArticlesServiceModel> DetailsOfArticleAsync(string articleId);

        Task<DetailsOfArticlesServiceModel> RateArticleAsync(string articleId, double rating);

        Task<DetailsOfArticlesServiceModel> CommentArticle(string articleId, string comment);
    }
}
