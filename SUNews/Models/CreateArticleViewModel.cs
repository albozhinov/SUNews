using System.ComponentModel.DataAnnotations;
using static SUNews.Data.DataConstants;

namespace SUNews.Models
{
    public class CreateArticleViewModel
    {
        [Required]
        [StringLength(ArticleContentMaxLength, MinimumLength = ArticleContentMinLength)]
        public string Content { get; init; }

        [Required]
        [StringLength(ArticleTitleMaxLength, MinimumLength = ArticleTitleMinLength)]
        public string Title { get; init; }

        [Required]
        [StringLength(ArticleImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(AuthorNameMaxLength, MinimumLength = AuthorNameMinLength)]
        public string AuthorName { get; set; }

        public ICollection<string> Categories { get; set; } = new List<string>();

    }
}
