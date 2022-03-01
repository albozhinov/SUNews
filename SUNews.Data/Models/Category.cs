namespace SUNews.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; init; }

        public ICollection<ArticleCategory> Articles { get; set; } = new List<ArticleCategory>();
    }
}
