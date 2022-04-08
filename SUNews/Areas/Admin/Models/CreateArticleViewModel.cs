using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static SUNews.Data.DataConstants;

namespace SUNews.Areas.Admin.Models
{
    public class CreateArticleViewModel
    {
        [Required]
        [StringLength(ArticleContentMaxLength, MinimumLength = ArticleContentMinLength, ErrorMessage = "Article content must be between {2} and {1}")]
        public string Content { get; init; }

        [Required]
        [StringLength(ArticleTitleMaxLength, MinimumLength = ArticleTitleMinLength, ErrorMessage = "Title must be between {2} and {1}")]
        public string Title { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        [StringLength(ArticleImageUrlMaxLength, ErrorMessage = "Image URL must be less than {1} characters.")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Author name")]
        [StringLength(AuthorNameMaxLength, MinimumLength = AuthorNameMinLength, ErrorMessage = "Author name must be between {2} and {1}")]
        public string AuthorName { get; set; }

        public ICollection<string> Categories { get; set; } = new List<string>();

        // This must to be here to list Categories in View!
        public IEnumerable<SelectListItem> CategoriesList { get; set; } = new List<SelectListItem>();
    }
}
