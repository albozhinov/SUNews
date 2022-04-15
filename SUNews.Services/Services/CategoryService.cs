namespace SUNews.Services.Services
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using SUNews.Data.Models;
    using SUNews.Data.Repository;
    using SUNews.Services.Contracts;
    using SUNews.Services.Models;
    using SUNews.Services.Providers;

    using static SUNews.Services.Constants.MessageConstant;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;
        private readonly IValidatorService validator;

        public CategoryService(IRepository _repository, IValidatorService validatorSerivce)
        {
            repository = _repository;
            validator = validatorSerivce;
        }

        public async Task<IEnumerable<CategoryServiceModel>> AllCategories()
        {
            return await repository.All<Category>()
                                   .Include(c => c.Articles)
                                   .Select(c => new CategoryServiceModel() 
                                   { 
                                       Id = c.Id,
                                       Name = c.Name,
                                       ArticlesCount = c.Articles.Count
                                   })
                                   .ToListAsync();

        }

        public async Task<string> CreateCategoryAsync(string categoryName)
        {
            validator.NullOrWhiteSpacesCheck(categoryName);

            var isExists = await repository.All<Category>()
                .FirstOrDefaultAsync(c => c.Name == categoryName.ToUpper());

            if (isExists != null)
            {
                return isExists.Name;
            }

            var categoryToAdd = new Category()
            {
                Name = categoryName.ToUpper(),
            };

            validator.ValidateModel(categoryToAdd);

            await repository.AddAsync(categoryToAdd);
            await repository.SaveAsync();

            return categoryToAdd.Name;
        }

        // TODO: This method must be edit! 
        public async Task<IEnumerable<CategoryServiceModel>> GetAllCategoriesAsync()
        {
            var allDbCategories = await repository.All<Category>()
                                                .Include(c => c.Articles)
                                                .ThenInclude(a => a.Article)
                                                .ToListAsync();

            var categoriesSM = allDbCategories
                                        .Select(c => new CategoryServiceModel(c))
                                        .ToList();

            return categoriesSM;
        }

        public async Task<IEnumerable<ArticleServiceModel>> GetArticlesByCategory(string id)
        {
            validator.NullOrWhiteSpacesCheck(id);

            (bool, Guid) isValidId = validator.TryParseGuid(id);

            if (!isValidId.Item1)
                throw new ArgumentNullException(CategoryNotFound);

            var articlesByCategory = await repository.All<ArticleCategory>()
                                                     .Where(ac => ac.CategoryId == isValidId.Item2 && !ac.Article.IsDeleted)
                                                     .Select(ac => new ArticleServiceModel()
                                                     {
                                                         Id = ac.ArticleId.ToString(),
                                                         Title = ac.Article.Title,
                                                         AuthorName = ac.Article.Author.Name,
                                                         Likes = ac.Article.LikeCount,
                                                         CommentsCount = ac.Article.Comments.Count
                                                     })
                                                     .ToListAsync();



                                                        //.Where(c => c.Id == isValidId.Item2)
                                                        //.Include(c => c.Articles)
                                                        //    .ThenInclude(ac => ac.Article)
                                                        //    .ThenInclude(a => a.Comments)
                                                        //.Include(c => c.Articles)
                                                        //    .ThenInclude(ac => ac.Article)
                                                        //    .ThenInclude(a => a.Author)
                                                        //.Select(c => c.Articles.Select(a => new ArticleServiceModel()
                                                        //{
                                                        //    Id = a.ArticleId.ToString(),
                                                        //    AuthorName = a.Article.Author.Name,
                                                        //    Title = a.Article.Title,
                                                        //    Likes = a.Article.LikeCount ?? 0,
                                                        //    CommentsCount = a.Article.Comments.Count,
                                                        //}))
                                                        //.FirstOrDefaultAsync();

            if (articlesByCategory is null)
                throw new ArgumentNullException(CategoryNotFound);


            return articlesByCategory;
        }        
    }
}
