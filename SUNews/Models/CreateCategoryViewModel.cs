namespace SUNews.Models
{
    using System.ComponentModel.DataAnnotations;
    using static SUNews.Data.DataConstants;

    public class CreateCategoryViewModel
    {
        [Required]
        [Display(Name = "Category name")]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, ErrorMessage = "Category name must be between {2} and {1} characters.")]
        public string CategoryName { get; set; }
    }
}
