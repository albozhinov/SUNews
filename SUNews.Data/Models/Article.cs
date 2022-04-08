namespace SUNews.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstants;

    public class Article
    {
        [Required]
        public Guid Id { get; init; } = Guid.NewGuid();
        
        [Required]
        [StringLength(ArticleContentMaxLength, MinimumLength = ArticleContentMinLength)]
        public string Content { get; set; }

        [Required]
        [StringLength(ArticleTitleMaxLength, MinimumLength = ArticleTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(ArticleImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        public int? LikeCount { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Like> UserLikes { get; set; } = new List<Like>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public Guid AuthorId { get; set; }

        public Author Author { get; set; }

        public DateTime DateOfCreation { get; init; } = DateTime.Now.Date;

        public ICollection<ArticleCategory> Categories { get; set; } = new List<ArticleCategory>();
    }
}
