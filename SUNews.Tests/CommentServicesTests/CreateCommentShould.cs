
namespace SUNews.Tests.CommentServicesTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MockQueryable.Moq;
    using Moq;
    using SUNews.Data.Models;
    using SUNews.Data.Repository;
    using SUNews.Services.Providers;
    using SUNews.Services.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using static SUNews.Tests.IncorrectData;

    [TestClass]
    public class CreateCommentShould
    {
        [TestMethod]
        [DataRow("", "Valid-Guid", "Valid-GUID")]
        [DataRow("  ", "Valid-Guid", "Valid-GUID")]
        [DataRow(null, "Valid-Guid", "Valid-GUID")]
        [DataRow("Valid-Text", "", "Valid-GUID")]
        [DataRow("Valid-Text", " ", "Valid-GUID")]
        [DataRow("Valid-Text", null, "Valid-GUID")]
        [DataRow("Valid-Text", "Valid-Guid", "")]
        [DataRow("Valid-Text", "Valid-Guid", "  ")]
        [DataRow("Valid-Text", "Valid-Guid", null)]
        [DataRow("Valid-Text", "INValid-Guid", "Valid-Guid")]
        public async Task ThrowArgumentNullException_WhenArgumentsAreEmptyWhiteSpacesOrNull(string commentText, string articleId, string userId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var sut = new CommentService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.CreateCommentAsync(commentText, articleId, userId));
        }

        [TestMethod]
        [DataRow("Valid-Text", "e4cf95e0-0b33-42f9-a81d-910bc305cb10", "Valid-Guid")]
        [DataRow("Valid-Text", "e4cf95e0-0b33-42f9-a81d-910bc305cb11", "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]

        public async Task ThrowArgumentException_WhenArticleNotExists(string commentText, string articleId, string userId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Id = Guid.Parse("e4cf95e0-0b33-42f9-a81d-910bc305cb11"),
            };

            var user = new User()
            {
                Id = "e4cf95e0-0b33-42f9-a81d-910bc305cb33"
            };

            repositoryStub.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryStub.Setup(a => a.All<User>()).Returns(new List<User>() { user }.AsQueryable().BuildMock());

            var sut = new CommentService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.CreateCommentAsync(commentText, articleId, userId));
        }

        [TestMethod]
        [DataRow("Six", "e4cf95e0-0b33-42f9-a81d-910bc305cb11", "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]
        [DataRow(InvalidCommentText, "e4cf95e0-0b33-42f9-a81d-910bc305cb11", "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]
        public async Task ThrowArgumentException_WhenCommentTextInvalid(string commentText, string articleId, string userId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Id = Guid.Parse(articleId),
            };

            var user = new User()
            {
                Id = userId
            };

            repositoryStub.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryStub.Setup(a => a.All<User>()).Returns(new List<User>() { user }.AsQueryable().BuildMock());

            var sut = new CommentService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.CreateCommentAsync(commentText, articleId, userId));
        }

        [TestMethod]
        [DataRow("Valid comment text!", "e4cf95e0-0b33-42f9-a81d-910bc305cb11", "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]
        public async Task CreateComment_WhenArgumentsAreValid(string commentText, string articleId, string userId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Id = Guid.Parse(articleId),
            };

            var user = new User()
            {
                Id = userId,
                FirstName = "Georgi",
                LastName = "Georgiev",
                UserName = "georgi@abv.bg"
            };

            repositoryStub.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryStub.Setup(a => a.All<User>()).Returns(new List<User>() { user }.AsQueryable().BuildMock());

            var sut = new CommentService(repositoryStub.Object, validatorServiceStub);

            // Act
            var createdComment = await sut.CreateCommentAsync(commentText, articleId, userId);

            // Assert
            Assert.IsTrue(createdComment.Text == commentText);
            Assert.IsTrue(createdComment.UserId == userId);
            Assert.IsTrue(createdComment.ArticleId == article.Id);

            repositoryStub.Verify(com => com.SaveAsync(), Times.Once);
        }
    }
}
