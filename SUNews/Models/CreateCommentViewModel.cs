using System.ComponentModel.DataAnnotations;

namespace SUNews.Models
{
    public class CreateCommentViewModel
    {
        [Required]
        [StringLength(150, MinimumLength = 6)]
        public string Text { get; init; }

        [Required]
        [StringLength(40)]
        public string ArticleId { get; set; }

        [Required]
        public string CurrentController { get; set; }

        [Required]
        public string CurrentAction { get; set; }
    }
}
