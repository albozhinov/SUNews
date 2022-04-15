namespace SUNews.Services.Services
{
    using Microsoft.EntityFrameworkCore;
    using SUNews.Data.Models;
    using SUNews.Data.Repository;
    using SUNews.Services.Contracts;
    using SUNews.Services.Models;
    using SUNews.Services.Providers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static SUNews.Services.Constants.MessageConstant;

    public class CommentService : ICommentService
    {
        private readonly IRepository repository;
        private readonly IValidatorService validator;

        public CommentService(IRepository _repository, IValidatorService _validator)
        {
            repository = _repository;
            validator = _validator;
        }

        public async Task<CommentServiceModel> CreateCommentAsync(string commentText, string articleId, string userId)
        {
            validator.NullOrWhiteSpacesCheck(commentText);
            validator.NullOrWhiteSpacesCheck(userId);
            validator.NullOrWhiteSpacesCheck(articleId);

            (bool, Guid) isArticleIdParsed = validator.TryParseGuid(articleId);

            if (!isArticleIdParsed.Item1)
                throw new ArgumentNullException(ArticleNotFound);

            var article = await repository.All<Article>()
                                          .FirstOrDefaultAsync(a => a.Id == isArticleIdParsed.Item2);
            if (article == null)
                throw new ArgumentException(ErrorArticleID);

            var user = await repository.All<User>().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException(ErrorUserID);


            var commentToAdd = new Comment()
            {
                Text = commentText,
                ArticleId = isArticleIdParsed.Item2,
                Article = article,
                UserId = userId,
                User = user
            };

            validator.ValidateModel(commentToAdd);

            await repository.AddAsync(commentToAdd);
            await repository.SaveAsync();
            return new CommentServiceModel(commentToAdd);
        }

        public async Task DeleteCommentAsync(int commnetId)
        {
            var commentToDeleted = await repository.All<Comment>().FirstOrDefaultAsync(c => c.Id == commnetId);

            if (commentToDeleted == null)
                throw new ArgumentException(ErrorCommentID);

            commentToDeleted.IsDeleted = true;

            repository.Update(commentToDeleted);
            await repository.SaveAsync();

            return;
        }

        public async Task<Comment> LikeCommentAsync(int commentId)
        {
            var commentToLiked = await repository.All<Comment>().FirstOrDefaultAsync(c => c.Id == commentId);

            if (commentToLiked == null)
                throw new ArgumentException(ErrorCommentID);

            commentToLiked.NumberOfVotes++;

            repository.Update(commentToLiked);
            await repository.SaveAsync();

            return commentToLiked;
        }

        public async Task ReportCommentAsync(int commentId)
        {
            var commentToReport = await repository.All<Comment>().FirstOrDefaultAsync(c => c.Id == commentId);

            if (commentToReport == null)
                throw new ArgumentException(ErrorCommentID);


            commentToReport.IsReported = true;
            repository.Update(commentToReport);
            await repository.SaveAsync();

            return;
        }

        public async Task<ICollection<CommentServiceModel>> GetAllReportedCommentAsync()
        {
            return await repository.All<Comment>()
                                                  .Where(c => c.IsReported && !c.IsDeleted)
                                                  .Include(c => c.User)
                                                  .Select(c => new CommentServiceModel()
                                                  {
                                                      Id = c.Id,
                                                      DateOfCreation = c.DateOfCreation,
                                                      Text = c.Text,
                                                      UserName = c.User.UserName,
                                                  })
                                                  .ToListAsync();
             
        }

        public async Task DeleteReportAsync(int commentId)
        {
            var reportedComment = await repository.All<Comment>().FirstOrDefaultAsync(c => c.Id == commentId);

            if (reportedComment == null)
                throw new ArgumentException(ErrorCommentID);


            reportedComment.IsReported = false;
            repository.Update(reportedComment);
            await repository.SaveAsync();

            return;
        }
    }
}
