namespace SUNews.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    using static SUNews.Data.DataConstants;

    public class CreateCategoryViewModel
    {
        [Required]
        [Display(Name = "Category name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Category name can contains only letters.")]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, ErrorMessage = "Category name must be between {2} and {1} characters.")]
        public string CategoryName { get; set; }
    }
}
