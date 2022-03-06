namespace SUNews.Services.Services
{
    using Microsoft.EntityFrameworkCore;
    using SUNews.Data.Models;
    using SUNews.Data.Repository;
    using SUNews.Services.Contracts;
    using System.Collections.Generic;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;

        public CategoryService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<string> CreateCategory(string categoryName)
        {
            var createdCategoryName = string.Empty;

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
