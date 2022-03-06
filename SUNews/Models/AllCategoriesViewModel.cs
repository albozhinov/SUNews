using SUNews.Data.Models;

namespace SUNews.Models
{
    public class AllCategoriesViewModel
    {
        public AllCategoriesViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Articles = category.Articles;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<ArticleCategory> Articles { get; set; }
    }
}
