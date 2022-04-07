namespace SUNews.Services.Models.Comment
{
    using SUNews.Data.Models;

    public class CommentServiceModel
    {
        public int Id { get; init; }

        public string Text { get; set; }

        public Guid ArticleId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int NumberOfVotes { get; set; }

        public DateTime DateOfCreation { get; init; } = DateTime.Now;

        public CommentServiceModel()
        {

        }

        public CommentServiceModel(Comment comment)
        {
            Id = comment.Id;
            Text = comment.Text;
            ArticleId = comment.ArticleId;
            UserId = comment.UserId;
            UserName = (comment.User.FirstName + " " + comment.User.LastName) ?? comment.User.UserName;
            NumberOfVotes = comment.NumberOfVotes;
            DateOfCreation = comment.DateOfCreation;

            //if (string.IsNullOrEmpty(comment.User.FirstName) && string.IsNullOrEmpty(comment.User.LastName))
            //{
            //    UserName = comment.User.UserName;
            //}
            //else
            //{
            //    UserName = (comment.User.FirstName + "" + comment.User.LastName);
            //}
        }
    }
}
