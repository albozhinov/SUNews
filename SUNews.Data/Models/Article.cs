namespace SUNews.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Article
    {
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        
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

        public string AuthorId { get; init; }

        public Author Author { get; init; }

        public ICollection<ArticleCategory> Categories { get; set; } = new List<ArticleCategory>();
    }
}
