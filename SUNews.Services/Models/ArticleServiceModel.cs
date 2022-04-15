namespace SUNews.Services.Models
{
    public class ArticleServiceModel
    {
        public string Id { get; set; }

        public string AuthorName { get; set; }

        public string Title { get; set; }

        public int? Likes { get; set; }

        public int CommentsCount { get; set; }
    }
}
