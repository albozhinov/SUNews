namespace SUNews.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Author
    {
        [Required]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [StringLength(AuthorNameMaxLength, MinimumLength = AuthorNameMinLength)]
        public string Name { get; init; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();

        public ICollection<User> Followers { get; set; } = new List<User>();
    }
}
