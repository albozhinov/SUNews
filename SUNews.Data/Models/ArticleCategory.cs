namespace SUNews.Data.Models
{
    public class ArticleCategory
    {
        public Guid ArticleId { get; set; }

        public Article Article { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
