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
    using SUNews.Services.Models;

    using static SUNews.Services.Constants.MessageConstant;

    public class ArticleService : IArticleService
    {
        private readonly IRepository repository;
        private readonly IValidatorService validator;        

        public ArticleService(IRepository _repository, IValidatorService validatorService)
        {
            this.repository = _repository;
            validator = validatorService;
        }
        
        public async Task<DetailsOfArticlesServiceModel> ArticleComments(string articleId, string commentText)
        {            
            validator.NullOrWhiteSpacesCheck(commentText);
            (bool, Guid) isValidId = validator.TryParseGuid(articleId);

            if (!isValidId.Item1)
                throw new ArgumentNullException(ArticleNotFound);

            var article = await repository.All<Article>()
                                                        .Where(a => a.Id == isValidId.Item2)
                                                        .Include(artCom => artCom.Comments)
                                                            .ThenInclude(com => com.Ratings)
                                                        .FirstOrDefaultAsync();


            return new DetailsOfArticlesServiceModel(article);
        }

        public async Task<DetailsOfArticlesServiceModel> CreateArticleAsync(string title, 
                                                                            string content,
                                                                            string imageUrl, 
                                                                            string authorName, 
                                                                            ICollection<string> categories)
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

            return new DetailsOfArticlesServiceModel(article);
        }

        public async Task<DetailsOfArticlesServiceModel> DetailsOfArticleAsync(string articleId)
        {
            (bool, Guid) isValidId = validator.TryParseGuid(articleId);

            if (!isValidId.Item1)
                throw new ArgumentNullException(ArticleNotFound);

            // Here maybe do somthing beteer... In UI we have "Show more" button who get Comment -> Comment
            var dbArticle = await repository.All<Article>()
                                                    .Include(artA => artA.Author)
                                                    .Include(artC => artC.Categories)                                                        
                                                    .FirstOrDefaultAsync(a => a.Id == isValidId.Item2);

            if (dbArticle == null)
                throw new ArgumentException(ArticleNotFound);


            return new DetailsOfArticlesServiceModel(dbArticle);
        }

        public async Task<ICollection<AllArticlesServiceModel>> GetAllArticlesAsync()
        {
            var allDBArticles = await repository.All<Article>()
                                                 .Include(a => a.Author)
                                                 .Include(a => a.Categories)
                                                 .ThenInclude(c => c.Category)
                                                 .Select(a => new Article()
                                                 {
                                                     Title = a.Title,
                                                     DateOfCreation = a.DateOfCreation,
                                                     ImageUrl = a.ImageUrl,
                                                     Rating = a.Rating,
                                                     Categories = a.Categories,
                                                     Author = a.Author
                                                 })
                                                 .ToListAsync();

            return allDBArticles.Select(a => new AllArticlesServiceModel(a)).ToList();
        }

        public async Task<DetailsOfArticlesServiceModel> RateArticleAsync(string articleId, double rating)
        {
            (bool, Guid) isValidId = validator.TryParseGuid(articleId);

            if (!isValidId.Item1)
                throw new ArgumentNullException(ArticleNotFound);

            var article = await repository.All<Article>().FirstOrDefaultAsync(a => a.Id == isValidId.Item2);

            if (article == null)
                throw new ArgumentException(ArticleNotFound);

            if (article.Rating == null)
            {
                article.Rating = rating;
            }

            article.Rating = (rating + article.Rating) / 2;

            article.LikeCount += 1;

            validator.ValidateModel(article);

            repository.Update(article);
            await repository.SaveAsync();

            return new DetailsOfArticlesServiceModel(article);
        }
    }
}
