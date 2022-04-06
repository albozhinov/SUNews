namespace SUNews.Data.Models
{
    public class AuthorUser
    {
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
