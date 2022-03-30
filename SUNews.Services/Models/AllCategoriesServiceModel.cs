using SUNews.Data.Models;

namespace SUNews.Services.Models
{
    public class AllCategoriesServiceModel
    {
        public AllCategoriesServiceModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Articles = category.Articles;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<ArticleCategory> Articles { get; set; }
    }
}
