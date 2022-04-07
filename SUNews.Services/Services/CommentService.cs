namespace SUNews.Services.Services
{
    using Microsoft.EntityFrameworkCore;
    using SUNews.Data.Models;
    using SUNews.Data.Repository;
    using SUNews.Services.Contracts;
    using SUNews.Services.Models.Comment;
    using SUNews.Services.Providers;
    using System;
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

        public async Task<bool> DeleteCommentAsync(int commnetId)
        {
            var commentToDeleted = await repository.All<Comment>().FirstOrDefaultAsync(c => c.Id == commnetId);

            if (commentToDeleted == null)
                throw new ArgumentException(ErrorCommentID);

            commentToDeleted.IsDeleted = true;

            repository.Update(commentToDeleted);
            await repository.SaveAsync();

            return true;
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

        public async Task<CommentReview> LikeCommentReviewAsync(int commentReviewId)
        {

            throw new NotImplementedException();
            //var commentReviewToLiked = await repository.All<CommentReview>()
            //                            .Include(cr => cr.Comment)
            //                            .FirstOrDefaultAsync(c => c.Id == commentReviewId);

            //if (commentReviewToLiked == null)
            //    throw new ArgumentException(ErrorCommentID);

            //commentReviewToLiked.Comment.NumberOfVotes++;

            //repository.Update(commentReviewToLiked);
            //await repository.SaveAsync();

            //return commentReviewToLiked;
        }

        public async Task<CommentReview> AddReviewToCommentAsync(int commentId, string userId, string reviviewText)
        {
            validator.NullOrWhiteSpacesCheck(userId);
            validator.NullOrWhiteSpacesCheck(reviviewText);

            var commentToBeReview = await repository.All<Comment>().FirstOrDefaultAsync(c => c.Id == commentId);
            if (commentToBeReview == null)
                throw new ArgumentException(ErrorCommentID);

            var user = await repository.All<User>().FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null)
                throw new ArgumentException(ErrorUserID);

            var newCommentReview = new Comment()
            {
                Text = reviviewText,
                UserId = userId,                
            };
            
            validator.ValidateModel(newCommentReview);

            var reviewToAdded = new CommentReview()
            {                
                UserId = userId,
                CommentId = newCommentReview.Id
            };

            commentToBeReview.Ratings.Add(reviewToAdded);


            //// TODO: Check Update -> Add newCommentReview and reviewToAdded in database!!!!
            repository.Update(commentToBeReview);
            //await repository.AddAsync(newCommentReview);
            //await repository.AddAsync(reviewToAdded);
            await repository.SaveAsync();

            return reviewToAdded;
        }
    }
}
