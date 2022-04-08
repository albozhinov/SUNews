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

        public Guid ArticleId { get; set; }

        public Article Article { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<CommentReview> Ratings { get; set; } = new List<CommentReview>();

        public int NumberOfVotes { get; set; }

        public DateTime DateOfCreation { get; init; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        public bool IsReported { get; set; } = false;
    }
}
