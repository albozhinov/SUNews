namespace SUNews.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Comment
    {
        public int Id { get; init; }

        [Required]
        [StringLength(CommentTextMaxLength, MinimumLength = CommentTextMinLength)]
        public string Text { get; init; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<CommentRatings> Ratings { get; set; } = new List<CommentRatings>();

        public int NumberOfVotes { get; set; }

        public bool IsDeleted { get; set; }
    }
}
