﻿namespace SUNews.Services.Services
{
    using Microsoft.EntityFrameworkCore;
    using SUNews.Data.Models;
    using SUNews.Data.Repository;
    using SUNews.Services.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class ArticleService : IArticleService
    {
        private readonly IRepository repository;

        public ArticleService(IRepository _repository)
        {
            this.repository = _repository;
        }

        public async Task<Article> CreateArticle(string title, string content, string imageUrl, string authorName, ICollection<string> categories)
        {
            var isExist = await repository.All<Article>().FirstOrDefaultAsync(a => a.Title == title.ToUpper());

            if (isExist != null)
            {
                return null;
            }

            var author = await repository.All<Author>().FirstOrDefaultAsync(a => a.Name.ToLower() == authorName.ToLower());

            if (author == null)
            {
                author = new Author()
                {
                    Name = authorName,
                };
            }

            var existsCategories = await repository.All<Category>()
                                                .Where(c => categories.Contains(c.Name))
                                                .ToListAsync();

            var article = new Article()
            {
                Title = title.ToUpper(),
                Content = content,
                ImageUrl = imageUrl,
                Author = author,
                Categories = existsCategories.Select(c => new ArticleCategory()
                {
                    CategoryId = c.Id                    
                }).ToList()
            };

            await repository.AddAsync(article);
            await repository.SaveAsync();

            return article;
        }
    }
}
