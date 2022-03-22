namespace SUNews.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ICollection<Article> FavoriteArticles { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<CommentReview> Ratings { get; set; }

        public ICollection<Author> FavoriteAuthors { get; set; }
    }
}
