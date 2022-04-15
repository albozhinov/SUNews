namespace SUNews.Models
{
    using SUNews.Services.Models;

    public class CategoriesAndArticlesViewModel
    {
        public string ChoosenCategory { get; set; }

        public IEnumerable<CategoryServiceModel> Categories { get; set; }

        public IEnumerable<ArticleServiceModel> Articles { get; set; }
    }
}
