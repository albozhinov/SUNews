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

    using static SUNews.Tests.IncorrectData;

    [TestClass]
    public class EditArticleShould
    {
        [DataTestMethod]
        [DataRow("", "Valid Title", "Valid Content", "Valid Url", "Valid AuthorName")]
        [DataRow("  ", "Valid Title", "Valid Content", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid string id", "", "Valid Content", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid string id", "  ", "Valid Content", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid string id", "Valid Title", "", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid string id", "Valid Title", "  ", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid string id", "Valid Title", "Valid Content", "", "Valid AuthorName")]
        [DataRow("Valid string id", "Valid Title", "Valid Content", "   ", "Valid AuthorName")]
        [DataRow("Valid string id", "Valid Title", "Valid Content", "Valid Url", "")]
        [DataRow("Valid string id", "Valid Title", "Valid Content", "Valid Url", "  ")]
        [DataRow(null, "Valid Title", "Valid Content", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid string id", null, "Valid Content", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid string id", "Valid Title", null, "Valid Url", "Valid AuthorName")]
        [DataRow("Valid string id", "Valid Title", "Valid Content", null, "Valid AuthorName")]
        [DataRow("Valid string id", "Valid Title", "Valid Content", "Valid Url", null)]
        public async Task ThrowArgumentNullException_WhenArgumentsAreEmptyWhiteSpacesOrNull(string articleId,
                                                                                            string title,
                                                                                            string content,
                                                                                            string imageUrl,
                                                                                            string authorName)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.EditArticleAsync(articleId,
                                                                                                              title,
                                                                                                              content,
                                                                                                              imageUrl,
                                                                                                              authorName,
                                                                                                              new List<string>()));
        }


        [TestMethod]
        [DataRow("Valid string id", "Valid Title", "Valid Content", "Valid Url", "Valid AuthorName")]
        public async Task ThrowArgumentNullException_WhenCategoriesArgIncorrect(string articleId,
                                                                                string title,
                                                                                string content,
                                                                                string imageUrl,
                                                                                string authorName)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.EditArticleAsync(articleId, 
                                                                                                              title,
                                                                                                              content,
                                                                                                              imageUrl,
                                                                                                              authorName,
                                                                                                              new List<string>()));

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.EditArticleAsync(articleId,
                                                                                                            title,
                                                                                                            content,
                                                                                                            imageUrl,
                                                                                                            authorName,
                                                                                                            null));

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.EditArticleAsync(articleId,
                                                                                                            title,
                                                                                                            content,
                                                                                                            imageUrl,
                                                                                                            authorName,
                                                                                                            new List<string>() { "" }));
        }


        [DataTestMethod]
        [DataRow("Invalid-GUID", "Valid Title", "Valid Content", "Valid Url", "Valid AuthorName")]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11", "Valid Title", "Valid Content", "Valid Url", "Valid AuthorName")]
        public async Task ThrowArgumentNullException_WhenGUIDIsIncorrect(string articleId,
                                                                     string title,
                                                                     string content,
                                                                     string imageUrl,
                                                                     string authorName)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Id = Guid.NewGuid(),
                Title = title.ToUpper(),
                Content = content,
                ImageUrl = imageUrl,
                Author = new Author() { Name = authorName },
            };


            repositoryMock.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());

            var sut = new ArticleService(repositoryMock.Object, validatorServiceStub);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.EditArticleAsync(articleId, 
                                                                                                        title,
                                                                                                        content,
                                                                                                        imageUrl,
                                                                                                        authorName,
                                                                                                        new List<string>() { "Category" }));
        }

        [DataTestMethod]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11", "Valid Title", "Valid Content", "Valid Url", "Teo")]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11", "Valid Title", "Valid Content", "Valid Url", "Invalid author name is between 5 and 40 characters!")]
        public async Task ThrowArgumentException_WhenAuthorNameIsIncorrect(string articleId,
                                                                           string title,
                                                                           string content,
                                                                           string imageUrl,
                                                                           string authorName)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var author = new Author() { Name = "Teodor" };

            var article = new Article()
            {
                Id = Guid.Parse("e4cf95e0-0b33-42f9-a81d-910bc305cb11"),
                Title = title.ToUpper(),
                Content = content,
                ImageUrl = imageUrl,
            };


            repositoryMock.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryMock.Setup(a => a.All<Author>()).Returns(new List<Author>() { author }.AsQueryable().BuildMock());


            var sut = new ArticleService(repositoryMock.Object, validatorServiceStub);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.EditArticleAsync(articleId,
                                                                                                        title,
                                                                                                        content,
                                                                                                        imageUrl,
                                                                                                        authorName,
                                                                                                        new List<string>() { "Category" }));
        }

        [DataTestMethod]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11", "Not", "Valid Content it's more 20 characters!", "Valid Url", "Teodor")]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11", InvalidTitle, "Valid Content it's more 20 characters!", "Valid Url", "Ivan Ivanov")]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11", "VALID TITLE", "Not", "Valid Url", "Ivan Ivanov")]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11", "VALID TITLE", InvalidContent, "Valid Url", "Ivan Ivanov")]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11", "VALID TITLE", "Valid Content it's more 20 characters!", InvalidURL, "Ivan Ivanov")]
        public async Task ThrowArgumentException_WhenArticleParamsAreIncorrect(string articleId,
                                                                               string title,
                                                                               string content,
                                                                               string imageUrl,
                                                                               string authorName)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var author = new Author() { Name = "Teodor" };

            var categories = new List<Category>() { new Category()
            {
                Name = "Category"
            },
            new Category()
            {
                Name = "Test Category"
            }
            };

            var article = new Article()
            {
                Id = Guid.Parse("e4cf95e0-0b33-42f9-a81d-910bc305cb11"),
                Title = title.ToUpper(),
                Content = content,
                ImageUrl = imageUrl,
            };


            repositoryMock.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryMock.Setup(a => a.All<Author>()).Returns(new List<Author>() { author }.AsQueryable().BuildMock());
            repositoryMock.Setup(a => a.All<Category>()).Returns( categories.AsQueryable().BuildMock());



            var sut = new ArticleService(repositoryMock.Object, validatorServiceStub);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.EditArticleAsync(articleId,
                                                                                                        title,
                                                                                                        content,
                                                                                                        imageUrl,
                                                                                                        authorName,
                                                                                                        new List<string>() { "Category" }));
        }

        [DataTestMethod]
        [DataRow("e4cf95e0-0b33-42f9-a81d-910bc305cb11", "VALID TITLE", "Valid Content it's more 20 characters!", "Valid Url", "Teodor")]
        public async Task EditArticle_WhenArticleParamsAreCorrect(string articleId,
                                                                  string title,
                                                                  string content,
                                                                  string imageUrl,
                                                                  string authorName)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var author = new Author() { Name = "Teodor" };

            var categories = new List<Category>() { new Category()
            {
                Name = "Category"
            },
            new Category()
            {
                Name = "Test Category"
            }
            };

            var article = new Article()
            {
                Id = Guid.Parse("e4cf95e0-0b33-42f9-a81d-910bc305cb11"),
                Title = title.ToUpper(),
                Content = content,
                ImageUrl = imageUrl,
            };


            repositoryMock.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryMock.Setup(a => a.All<Author>()).Returns(new List<Author>() { author }.AsQueryable().BuildMock());
            repositoryMock.Setup(a => a.All<Category>()).Returns(categories.AsQueryable().BuildMock());

            var sut = new ArticleService(repositoryMock.Object, validatorServiceStub);

            //Act
            await sut.EditArticleAsync(articleId, title, content, imageUrl, authorName, new List<string>() { "Category" });

            // Assert
            repositoryMock.Verify(art => art.Update(article), Times.Once);
            repositoryMock.Verify(mm => mm.SaveAsync(), Times.Once);
        }
    }
}
