namespace SUNews.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Article
    {
        [Required]
        public Guid Id { get; init; } = Guid.NewGuid();
        
        [Required]
        [StringLength(ArticleContentMaxLength, MinimumLength = ArticleContentMinLength)]
        public string Content { get; init; }

        [Required]
        [StringLength(ArticleTitleMaxLength, MinimumLength = ArticleTitleMinLength)]
        public string Title { get; init; }

        [Required]
        [StringLength(ArticleImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        public int? LikeCount { get; set; }

        [Range(ArticleRatingMin, ArticleRatingMax)]
        public double? Rating { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public Guid AuthorId { get; init; }

        public Author Author { get; init; }

        public DateTime DateOfCreation { get; init; } = DateTime.Now.Date;

        public ICollection<ArticleCategory> Categories { get; set; } = new List<ArticleCategory>();
    }
}
