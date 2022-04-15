namespace SUNews.Services.Contracts
{
    using SUNews.Data.Models;
    using SUNews.Services.Models;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<CommentServiceModel> CreateCommentAsync(string commentText, string articleId, string userId);

        Task DeleteCommentAsync(int commentId);

        Task ReportCommentAsync(int commentId);

        Task<Comment> LikeCommentAsync(int commentId);

        Task<ICollection<CommentServiceModel>> GetAllReportedCommentAsync();

        Task DeleteReportAsync(int commentId);
    }
}
