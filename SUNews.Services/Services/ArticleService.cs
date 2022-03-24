namespace SUNews.Services.Services
{
    using Microsoft.EntityFrameworkCore;
    using SUNews.Data.Models;
    using SUNews.Data.Repository;
    using SUNews.Services.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using SUNews.Services.Providers;
    using System;

    public class ArticleService : IArticleService
    {
        private readonly IRepository repository;
        private readonly IValidatorService validator;

        public ArticleService(IRepository _repository, IValidatorService validatorService)
        {
            this.repository = _repository;
            validator = validatorService;
        }


        /// <summary>
        /// This must be implement!
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Article> CommentArticle(Guid articleId, string comment)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> CreateArticleAsync(string title, string content, string imageUrl, string authorName, ICollection<string> categories)
        {
            validator.NullOrWhiteSpacesCheck(title);
            validator.NullOrWhiteSpacesCheck(content);
            validator.NullOrWhiteSpacesCheck(imageUrl);
            validator.NullOrWhiteSpacesCheck(authorName);
            validator.NullOrEmptyCollection(categories);


            var isExist = await repository.All<Article>().FirstOrDefaultAsync(a => a.Title == title.ToUpper());

            if (isExist != null)
            {
                return null;
            }

            var author = await repository.All<Author>().FirstOrDefaultAsync(a => a.Name.ToLower() == authorName.ToLower());

            ///Автора не се добавя тук.
            ///Ако не съществува базата данни хвърля грешка
            ///Бизнес логиката ще бъде автора да е логнат и той да създава статия, тогава 
            ///в метода CreateArticleAsync ще получаваме и AuthorId параметър.
            //if (author == null)
            //{
            //    author = new Author()
            //    {
            //        Name = authorName,
            //    };
            //}

            var existsCategories = await repository.All<Category>()
                                                .Where(c => categories.Contains(c.Name))
                                                .ToListAsync();

            var article = new Article()
            {
                Title = title.ToUpper(),
                Content = content,
                ImageUrl = imageUrl,
                AuthorId = author.Id,
                Categories = existsCategories.Select(c => new ArticleCategory()
                {
                    CategoryId = c.Id,

                }).ToList()
            };

            // Here we validate all model's properties constraints
            validator.ValidateModel(article);

            await repository.AddAsync(article);
            await repository.SaveAsync();

            return article;
        }

        public async Task<Article> DetailsOfArticleAsync(Guid articleId)
        {

            // Here maybe do somthing beteer... In UI we have "Show more" button who get Comment -> Comment
            var article = await repository.All<Article>()
                                                    .Include(artA => artA.Author)
                                                    .Include(artC => artC.Categories)
                                                    .Include(artCom => artCom.Comments)
                                                        .ThenInclude(com => com.Ratings)                                                        
                                                    .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                throw new ArgumentException("The article cannot be find.");


            return article;
        }

        public async Task<ICollection<Article>> GetAllArticlesAsync()
        {
            return await repository.All<Article>()
                                                 .Include(a => a.Author)
                                                 .Include(a => a.Categories)
                                                 .ThenInclude(c => c.Category)
                                                 .Select(a => new Article()
                                                 {
                                                     Title = a.Title,
                                                     Content = a.Content,
                                                     DateOfCreation = a.DateOfCreation,
                                                     ImageUrl = a.ImageUrl,
                                                     Rating = a.Rating,
                                                     Categories = a.Categories,
                                                 })
                                                 .ToListAsync();
        }

        public async Task<Article> RateArticleAsync(Guid articleId, double rating)
        {
            var article = await repository.All<Article>().FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                throw new ArgumentException($"This article: {articleId} cannot find.");

            if (article.Rating == null)
            {
                article.Rating = rating;
            }

            article.Rating = (rating + article.Rating) / 2;

            article.LikeCount += 1;

            validator.ValidateModel(article);

            repository.Update(article);
            await repository.SaveAsync();

            return article;
        }
    }
}
