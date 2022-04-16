namespace SUNews.Tests.ArticleServicesTests
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
    using System.Threading.Tasks;

    [TestClass]
    public class LikeArticleShould
    {
        [DataTestMethod]
        [DataRow(null, "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]
        [DataRow("", "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]
        [DataRow(" ", "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]
        [DataRow("Invalid-GUID", "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb22", null)]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb22", "")]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb22", " ")]
        public async Task ThrowArgumentNullException_WhenArgumentIsInvalid(string articleId, string userId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.LikeArticleAsync(articleId, userId));
        }

        [DataTestMethod]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb33", "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb55", "e4cf95e0-0b33-42f9-a81d-910bc305cb44")]

        public async Task ThrowArgumentException_WhenArticleOrUserNotExists(string articleId, string userId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Id = Guid.Parse("e4cf95e0-0b33-42f9-a81d-910bc305cb55"),
                Title = "VALID TITLE",
                Content = "Valid content must be between 20 and 100 characters!",
                ImageUrl = "Valid URL",
                IsDeleted = false,
                Author = new Author() { Name = "Ivan Ivanov" },
                UserLikes = new List<Like>() { new Like() { Id = 11, UserId = userId } },
            };

            var user = new User()
            {
                Id = "e4cf95e0-0b33-42f9-a81d-910bc305cb22",
                UserName = "Ivan Ivanov"
            };


            repositoryStub.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryStub.Setup(a => a.All<User>()).Returns(new List<User>() { user }.AsQueryable().BuildMock());


            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.LikeArticleAsync(articleId, userId));
        }

        [DataTestMethod]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb33", "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]
        public async Task LikeArticle_WhenUserIsNotExists(string articleId, string userId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Id = Guid.Parse(articleId),
                Title = "VALID TITLE",
                Content = "Valid content must be between 20 and 100 characters!",
                ImageUrl = "Valid URL",
                IsDeleted = false,
                LikeCount = 5,
                Author = new Author() { Name = "Ivan Ivanov" },
                UserLikes = new List<Like>() { new Like() { Id = 11, UserId = "e4cf95e0-0b33-42f9-a81d-910bc305cb25" } },
            };

            var user = new User()
            {
                Id = userId,
                UserName = "Ivan Ivanov"
            };


            repositoryStub.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryStub.Setup(a => a.All<User>()).Returns(new List<User>() { user }.AsQueryable().BuildMock());


            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act
            var likedArticle = await sut.LikeArticleAsync(articleId, userId);

            // Assert


            Assert.IsTrue(likedArticle.Likes == 6);
            repositoryStub.Verify(art => art.Update(article), Times.Once);
            repositoryStub.Verify(mm => mm.SaveAsync(), Times.Once);

        }

        [DataTestMethod]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb33", "e4cf95e0-0b33-42f9-a81d-910bc305cb22")]
        public async Task UnlikeArticle_WhenUserIstExists(string articleId, string userId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Id = Guid.Parse(articleId),
                Title = "VALID TITLE",
                Content = "Valid content must be between 20 and 100 characters!",
                ImageUrl = "Valid URL",
                IsDeleted = false,
                LikeCount = 5,
                Author = new Author() { Name = "Ivan Ivanov" },
                UserLikes = new List<Like>() { new Like() { Id = 11, UserId = userId} },
            };

            var user = new User()
            {
                Id = userId,
                UserName = "Ivan Ivanov"
            };


            repositoryStub.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryStub.Setup(a => a.All<User>()).Returns(new List<User>() { user }.AsQueryable().BuildMock());


            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act
            var likedArticle = await sut.LikeArticleAsync(articleId, userId);

            // Assert


            Assert.IsTrue(likedArticle.Likes == 4);
            repositoryStub.Verify(art => art.Update(article), Times.Once);
            repositoryStub.Verify(mm => mm.SaveAsync(), Times.Once);
        }

    }
}
