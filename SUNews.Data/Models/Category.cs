namespace SUNews.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Category
    {
        [MaxLength(IdMaxLength)]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public String Name { get; init; }

        public ICollection<ArticleCategory> Articles { get; set; } = new List<ArticleCategory>();
    }
}
