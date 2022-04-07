using SUNews.Data.Models;

namespace SUNews.Services.Models
{
    public class CategoryServiceModel
    {
        public CategoryServiceModel(Category model)
        {
            Id = model.Id;
            Name = model.Name;
            Articles = model.Articles.Select(a => new AllArticlesServiceModel()
            {
                Id = a.ArticleId,
                Title = a.Article.Title,
                ImageUrl = a.Article.ImageUrl,
                DateOfCreation = a.Article.DateOfCreation,
                Likes = a.Article.LikeCount ?? 0,

            }).ToList();
        }

        public Guid Id { get; init; }

        public string Name { get; init; }

        public ICollection<AllArticlesServiceModel> Articles { get; set; } = new List<AllArticlesServiceModel>();

    }
}
