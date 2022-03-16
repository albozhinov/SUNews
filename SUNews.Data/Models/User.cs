namespace SUNews.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public ICollection<Article> FavoriteArticles { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<CommentReview> Ratings { get; set; }

        public ICollection<Author> FavoriteAuthors { get; set; }
    }
}
