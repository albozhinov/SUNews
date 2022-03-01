namespace SUNews.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class CommentRatings
    {
        public int Id { get; init; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int CommentId { get; set; }

        public Comment Comment { get; set; }

        [Required]
        [Range(ArticleRatingMin, ArticleRatingMax)]
        public double CommentRating { get; set; }
    }
}
