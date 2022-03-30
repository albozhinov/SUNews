using SUNews.Data.Models;

namespace SUNews.Services.Models
{
    public class DetailsOfArticlesServiceModel
    {
        public Guid Id { get; init; }

        public string Content { get; init; }

        public string Title { get; init; }

        public string ImageUrl { get; init; }

        public int Likes { get; init; }

        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();

        public Guid AuthorId { get; init; }

        public string AuthorName { get; init; }

        public DateTime DateOfCreation { get; init; }

        public IEnumerable<ArticleCategory> Categories { get; set; } = new List<ArticleCategory>();


        public DetailsOfArticlesServiceModel()
        {

        }

        public DetailsOfArticlesServiceModel(Article model)
        {
            Id = model.Id;
            Content = model.Content;
            Title = model.Title;
            ImageUrl = model.ImageUrl;
            Likes = model.LikeCount ?? 0;
            Comments = model.Comments;
            AuthorId = model.AuthorId;
            AuthorName = model.Author.Name;
            DateOfCreation = model.DateOfCreation;
            Categories = model.Categories;
        }
    }
}
