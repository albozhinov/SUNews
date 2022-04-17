namespace SUNews.Tests.CategoryServicesTests
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
    public class AllCategoriesShould
    {
        [TestMethod]
        public async Task GetAllCategories_WhenArticleNotDeleted()
        {
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new Mock<IValidatorService>();

            var firstArticleId = "e4cf95e0-0b33-42f9-a81d-910bc305cb11";

            var firstCategoryId = "e4cf95e0-0b33-42f9-a81d-910bc305cb12";


            var firstCategory = new Category()
            {
                Id = Guid.Parse(firstCategoryId),
                Name = "ValidCategory",
                Articles = new List<ArticleCategory>()
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

            firstCategory.Articles.Add(firstArticle.Categories.First());

            repositoryMock.Setup(a => a.All<Category>()).Returns(new List<Category>() { firstCategory }.AsQueryable().BuildMock());

            var sut = new CategoryService(repositoryMock.Object, validatorServiceStub.Object);

            // Act
            var articleCreateSut = await sut.AllCategories();
            var testedCategory = articleCreateSut.First();

            // Assert
            Assert.IsTrue(testedCategory.ArticlesCount == 0);
            Assert.IsTrue(testedCategory.Id == firstCategory.Id);
            Assert.IsTrue(testedCategory.Name == firstCategory.Name);
        }

        [TestMethod]
        public async Task GetAllCategories_WhenArticleIsDeleted()
        {
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new Mock<IValidatorService>();

            var firstArticleId = "e4cf95e0-0b33-42f9-a81d-910bc305cb11";

            var firstCategoryId = "e4cf95e0-0b33-42f9-a81d-910bc305cb12";


            var firstCategory = new Category()
            {
                Id = Guid.Parse(firstCategoryId),
                Name = "ValidCategory",
                Articles = new List<ArticleCategory>()
            };

            var firstArticle = new Article()
            {
                Id = Guid.Parse(firstArticleId),
                Title = "VALID TITLE",
                Content = "Valid Content it's more 20 characters!",
                ImageUrl = "Valid Url",
                IsDeleted = true,
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

            repositoryMock.Setup(a => a.All<Category>()).Returns(new List<Category>() { firstCategory }.AsQueryable().BuildMock());

            var sut = new CategoryService(repositoryMock.Object, validatorServiceStub.Object);

            // Act
            var articleCreateSut = await sut.AllCategories();
            var testedCategory = articleCreateSut.First();

            // Assert
            Assert.IsTrue(testedCategory.ArticlesCount == 0);
            Assert.IsTrue(testedCategory.Id == firstCategory.Id);
            Assert.IsTrue(testedCategory.Name == firstCategory.Name);
        }
    }
}
