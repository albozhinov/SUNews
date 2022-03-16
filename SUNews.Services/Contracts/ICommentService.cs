namespace SUNews.Services.Contracts
{
    using SUNews.Data.Models;
    using System;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(string commentText, Guid articleId, string userId);

        Task<bool> DeleteCommentAsync(int commentId);

        Task<Comment> LikeCommentAsync(int commentId);

        Task<CommentReview> AddReviewToCommentAsync(int commentId, string userId, string reviviewText);

        Task<CommentReview> LikeCommentReviewAsync(int commentReviewId);
    }
}
