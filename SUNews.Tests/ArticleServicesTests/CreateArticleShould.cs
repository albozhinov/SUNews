using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using SUNews.Data.Context;
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

namespace SUNews.Tests.ArticleServicesTests
{
    [TestClass]
    public class CreateArticleShould
    {
        private DbContextOptions<SUNewsDbContext> contextOptions;

        [DataTestMethod]
        [DataRow("", "Valid Content", "Valid Url", "Valid AuthorName")]
        [DataRow("  ", "Valid Content", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid Title", "", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid Title", "  ", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid Title", "Valid Content", "", "Valid AuthorName")]
        [DataRow("Valid Title", "Valid Content", "   ", "Valid AuthorName")]
        [DataRow("Valid Title", "Valid Content", "Valid Url", "")]
        [DataRow("Valid Title", "Valid Content", "Valid Url", "  ")]
        [DataRow(null, "Valid Content", "Valid Url", "Valid AuthorName")]
        [DataRow("Valid Title", null, "Valid Url", "Valid AuthorName")]
        [DataRow("Valid Title", "Valid Content", null, "Valid AuthorName")]
        [DataRow("Valid Title", "Valid Content", "Valid Url", null)]
        public async Task ThrowArgumentException_WhenArgumentsAreEmptyWhiteSpacesOrNull(string title,
                                                                            string content,
                                                                            string imageUrl,
                                                                            string authorName)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.CreateArticleAsync(title,
                                                                                                              content,
                                                                                                              imageUrl,
                                                                                                              authorName,
                                                                                                              new List<string>()));
        }

        [TestMethod]
        [DataRow("Valid Title", "Valid Content", "Valid Url", "Valid AuthorName")]
        public async Task ThrowArgumentException_WhenCategoriesArgIncorrect(string title,
                                                                            string content,
                                                                            string imageUrl,
                                                                            string authorName)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var sut = new ArticleService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.CreateArticleAsync(title,
                                                                                                              content,
                                                                                                              imageUrl,
                                                                                                              authorName,
                                                                                                              new List<string>()));

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.CreateArticleAsync(title,
                                                                                                              content,
                                                                                                              imageUrl,
                                                                                                              authorName,
                                                                                                              null));

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.CreateArticleAsync(title,
                                                                                                              content,
                                                                                                              imageUrl,
                                                                                                              authorName,
                                                                                                              new List<string>() { "" }));
        }

        [DataTestMethod]
        [DataRow("Title allready exists!", "Valid Content", "Valid Url", "Valid AuthorName")]
        public async Task ThrowArgumentException_WhenArticleExists(string title,
                                                                   string content,
                                                                   string imageUrl,
                                                                   string authorName)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Title = title.ToUpper(),
                Content = content,
                ImageUrl = imageUrl,
                Author = new Author() { Name = authorName },
            };


            repositoryMock.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());

            var sut = new ArticleService(repositoryMock.Object, validatorServiceStub);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.CreateArticleAsync(title,
                                                                                                              content,
                                                                                                              imageUrl,
                                                                                                              authorName,
                                                                                                              new List<string>() { "Category" }));
        }

        [DataTestMethod]
        [DataRow("Title allready exists!", "Valid Content", "Valid Url", "Valid AuthorName")]
        public async Task ThrowArgumentException_WhenCategoriesNotExists(string title,
                                                                         string content,
                                                                         string imageUrl,
                                                                         string authorName)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Title = "TestTitle",
                Content = content,
                ImageUrl = imageUrl,                
            };

            var categories = new List<Category>() 
            {
                new Category()
                {
                    Name = "Category",
                },
                new Category()
                {
                    Name = "TestCategory",
                }
            };


            repositoryMock.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryMock.Setup(c => c.All<Category>()).Returns(categories.AsQueryable().BuildMock());
            repositoryMock.Setup(a => a.All<Author>()).Returns(new List<Author>() { new Author() { Name = authorName } }.AsQueryable().BuildMock());


            var sut = new ArticleService(repositoryMock.Object, validatorServiceStub);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.CreateArticleAsync(title,
                                                                                                              content,
                                                                                                              imageUrl,
                                                                                                              authorName,
                                                                                                              new List<string>() { "ValidCategory" }));
        }





        [DataTestMethod]
        [DataRow("Not", "Valid Content it's more 20 characters!", "Valid Url", "Valid AuthorName")]
        [DataRow(InvalidTitle, "Valid Content it's more 20 characters!", "Valid Url", "Valid AuthorName")]
        [DataRow("Valit Title", "Not Valid", "Valid Url", "Valid AuthorName")]
        [DataRow("Valit Title", InvalidContent, "Valid Url", "Valid AuthorName")]
        [DataRow("Valit Title", "Valid Content it's more 20 characters!", InvalidURL, "Valid AuthorName")]
        [DataRow("Valit Title", "Valid Content it's more 20 characters!", "Valid Url", "Teo")]
        [DataRow("Valit Title", "Valid Content it's more 20 characters!", "Valid Url", "Invalid author name is between 5 and 40 characters!")]
        public async Task ThrowArgumentException_WhenArgumentsIncorrect(string title,
                                                                         string content,
                                                                         string imageUrl,
                                                                         string authorName)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Title = "TestTitle",
                Content = content,
                ImageUrl = imageUrl,
            };

            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "ValidCategory",
                },
                new Category()
                {
                    Name = "TestCategory",
                }
            };


            repositoryMock.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryMock.Setup(c => c.All<Category>()).Returns(categories.AsQueryable().BuildMock());
            repositoryMock.Setup(a => a.All<Author>()).Returns(new List<Author>() { new Author() { Name = "Invalid Name" } }.AsQueryable().BuildMock());


            var sut = new ArticleService(repositoryMock.Object, validatorServiceStub);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.CreateArticleAsync(title,
                                                                                                              content,
                                                                                                              imageUrl,
                                                                                                              authorName,
                                                                                                              new List<string>() { "ValidCategory" }));
        }

        [DataTestMethod]
        [DataRow("Valid Title", "Valid Content it's more 20 characters!", "Valid Url", "Teodor")]
        public async Task CreateArticle_WhenArgumentsCorrect(string title,
                                                             string content,
                                                             string imageUrl,
                                                             string authorName)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var article = new Article()
            {
                Title = "TestTitle",
                Content = content,
                ImageUrl = imageUrl,
            };

            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "ValidCategory",
                },
                new Category()
                {
                    Name = "TestCategory",
                }
            };


            repositoryMock.Setup(a => a.All<Article>()).Returns(new List<Article>() { article }.AsQueryable().BuildMock());
            repositoryMock.Setup(c => c.All<Category>()).Returns(categories.AsQueryable().BuildMock());
            repositoryMock.Setup(a => a.All<Author>()).Returns(new List<Author>() { new Author() { Name = "Invalid Name" } }.AsQueryable().BuildMock());

            var sut = new ArticleService(repositoryMock.Object, validatorServiceStub);

            // Act
            var articleCreateSut = await sut.CreateArticleAsync(title, content, imageUrl, authorName, new List<string>() { "ValidCategory"});

            // Assert
            Assert.IsTrue(articleCreateSut.Title == title.ToUpper());
            Assert.IsTrue(articleCreateSut.Content == content);
            Assert.IsTrue(articleCreateSut.ImageUrl == imageUrl);
            Assert.IsTrue(articleCreateSut.Categories.Count() == 1);
            repositoryMock.Verify(mm => mm.SaveAsync(), Times.Once);
        }
    }
}
