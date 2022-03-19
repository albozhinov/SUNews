using SUNews.Data.Models;

namespace SUNews.Models
{
    public class AllArticlesViewModel
    {
        public Guid Id { get; init; }

        public string Content { get; init; }

        public string Title { get; init; }

        public string ImageUrl { get; init; }

        public int Likes { get; init; }

        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();

        public Guid AuthorId { get; init; }

        public DateTime DateOfCreation { get; init; }

        public IEnumerable<ArticleCategory> Categories { get; set; } = new List<ArticleCategory>();


        public AllArticlesViewModel(Article model)
        {
            Id = model.Id;
            Content = model.Content;
            Title = model.Title;
            ImageUrl = model.ImageUrl;
            AuthorId = model.AuthorId;
            DateOfCreation = model.DateOfCreation;
            Categories = model.Categories;
            Comments = model.Comments;
            Likes = model.LikeCount ?? 0;
        }
    }
}
