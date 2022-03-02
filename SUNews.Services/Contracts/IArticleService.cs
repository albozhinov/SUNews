namespace SUNews.Services.Contracts
{
    using SUNews.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<Article> CreateArticle();
    }
}
