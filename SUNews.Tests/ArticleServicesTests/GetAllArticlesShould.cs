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
    public class GetAllArticlesShould
    {
        [TestMethod]
        public async Task GetAllArticle_WhenAllValid()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new Mock<IValidatorService>();

            var firstArticleId = "e4cf95e0-0b33-42f9-a81d-910bc305cb11";
            var secondArticleId = "e4cf95e0-0b33-42f9-a81d-910bc305cb10";

            var firstCategoryId = "e4cf95e0-0b33-42f9-a81d-910bc305cb12";
            var secondCategoryId = "e4cf95e0-0b33-42f9-a81d-910bc305cb13";


            var firstCategory = new Category()
            {
                Id = Guid.Parse(firstCategoryId),
                Name = "ValidCategory",
            };

            var secondCategory = new Category()
            {
                Id = Guid.Parse(secondCategoryId),
                Name = "TestCategory",
            };

            var firstArticle = new Article()
            {
                Id = Guid.Parse(firstArticleId),
                Title = "VALID TITLE",
                Content = "Valid Content it's more 20 characters!",
                ImageUrl = "Valid Url",
                IsDeleted = false,
                Author = new Author() { Name = "Ivan Ivanov" },
                Categories = new List<ArticleCategory>()
                {
                    new ArticleCategory()
                    {
                        ArticleId = Guid.Parse(firstArticleId),
                        CategoryId = Guid.Parse(firstCategoryId),
                        Category = firstCategory,
                    }
                }
            };

            var secondArticle = new Article()
            {
                Id = Guid.Parse(firstArticleId),
                Title = "VALID TITLE 123",
                Content = "Valid Content it's more 20 characters! Valid Content it's more 20 characters!",
                ImageUrl = "Valid Urllllllll",
                IsDeleted = true,
                Author = new Author() { Name = "Ivan Todorov" },
                Categories = new List<ArticleCategory>()
                {
                    new ArticleCategory()
                    {
                        ArticleId = Guid.Parse(secondArticleId),
                        CategoryId = Guid.Parse(secondCategoryId),
                        Category = secondCategory,
                    }
                }
            };

            var allArticles = new List<Article>() { firstArticle, secondArticle };

            repositoryMock.Setup(a => a.All<Article>()).Returns(new List<Article>() { firstArticle }.AsQueryable().BuildMock());

            var sut = new ArticleService(repositoryMock.Object, validatorServiceStub.Object);

            // Act
            var articleCreateSut = await sut.GetAllArticlesAsync();
            var testedArticle = articleCreateSut.First();

            // Assert
            Assert.IsTrue(testedArticle.Id == firstArticle.Id);
            Assert.IsTrue(testedArticle.Title == firstArticle.Title);
            Assert.IsTrue(testedArticle.ImageUrl == firstArticle.ImageUrl);
            //Assert.IsTrue(testedArticle.Categories.Count() == 1);
            Assert.IsTrue(testedArticle.AuthorName == firstArticle.Author.Name);
        }
    }
}
