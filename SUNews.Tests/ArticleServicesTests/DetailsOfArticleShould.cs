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
    public class DetailsOfArticleShould
    {
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("Invalid-GUID")]
        public async Task ThrowArgumentNullException_WhenArgumentIsInvalid(string articleId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.DetailsOfArticleAsync(articleId));
        }

        [DataTestMethod]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11")]
        public async Task ThrowArgumentException_WhenArticleNotExists(string articleId)
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
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.DetailsOfArticleAsync(articleId));
        }


        [DataTestMethod]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11")]
        public async Task ReturnArticle_WhenArticleExists(string articleId)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Id = Guid.Parse("e4cf95e0-0b33-42f9-a81d-910bc305cb11"),
                Title = "TEST TITLE",
                Content = "Valid content is between 20 and 1000 characters!",
                ImageUrl = "Valid URL",
                Author = new Author() { Name = "Goshecneto"}
            };

            repositoryStub.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());

            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act
            var articleDetails = await sut.DetailsOfArticleAsync(articleId);

            // Assert
            Assert.IsTrue(articleDetails.Title == article.Title);
            Assert.IsTrue(articleDetails.Content == article.Content);
            Assert.IsTrue(articleDetails.ImageUrl == article.ImageUrl);
        }


    }
}
