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
    public class DeleteArticleShould
    {
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        public async Task ThrowArgumentException_WhenArgumentsAreNull(string articleId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.DeleteArticleAsync(articleId));
        }

        [DataTestMethod]
        [DataRow("Not-valid-GUID")]
        public async Task ThrowArgumentException_WhenArgumentsAreIncorrect(string articleId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();


            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.DeleteArticleAsync(articleId));
        }

        [DataTestMethod]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11")]
        public async Task ThrowArgumentException_WhenArticleNotFound(string articleId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Id = Guid.NewGuid(),
                Title = "TestTitle",
                Content = "Valid content is between 20 and 1000 characters!",
                ImageUrl = "Valid URL",
            };

            repositoryStub.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());

            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);


            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.DeleteArticleAsync(articleId));
        }

        [DataTestMethod]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11")]
        public async Task DeleteArticle_WhenArgumentIsCorrect(string articleId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Id = Guid.Parse(articleId),
                Title = "TestTitle",
                Content = "Valid content is between 20 and 1000 characters!",
                ImageUrl = "Valid URL",
                IsDeleted = false,
            };

            repositoryStub.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());

            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act
            await sut.DeleteArticleAsync(articleId);

            // Assert
            repositoryStub.Verify(art => art.Update(article), Times.Once);
            repositoryStub.Verify(art => art.SaveAsync(), Times.Once);
        }
    }
}
