namespace SUNews.Services.Contracts
{
    using SUNews.Data.Models;
    using SUNews.Services.Models.Comment;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<CommentServiceModel> CreateCommentAsync(string commentText, string articleId, string userId);

        Task DeleteCommentAsync(int commentId);

        Task<Comment> LikeCommentAsync(int commentId);

        Task<CommentReview> AddReviewToCommentAsync(int commentId, string userId, string reviviewText);

        Task<CommentReview> LikeCommentReviewAsync(int commentReviewId);
    }
}
