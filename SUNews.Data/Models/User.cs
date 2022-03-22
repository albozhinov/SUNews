namespace SUNews.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class User : IdentityUser
    {
        [MaxLength(FirstAndLastNameLength)]
        public string? FirstName { get; set; }

        [MaxLength(FirstAndLastNameLength)]
        public string? LastName { get; set; }

        public ICollection<Article> FavoriteArticles { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<CommentReview> Ratings { get; set; }

        public ICollection<Author> FavoriteAuthors { get; set; }
    }
}
