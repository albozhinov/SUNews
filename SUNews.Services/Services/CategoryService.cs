namespace SUNews.Services.Services
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using SUNews.Data.Models;
    using SUNews.Data.Repository;
    using SUNews.Services.Contracts;
    using SUNews.Services.Providers;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;
        private readonly IValidatorService validator;

        public CategoryService(IRepository _repository, IValidatorService validatorSerivce)
        {
            repository = _repository;
            validator = validatorSerivce;
        }

        public async Task<string> CreateCategory(string categoryName)
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

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await repository.All<Category>()
                                                .Include(c => c.Articles)
                                                .ThenInclude(a => a.Article)
                                                .ToListAsync();
        }
    }
}
