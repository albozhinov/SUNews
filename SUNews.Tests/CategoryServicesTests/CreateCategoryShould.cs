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
    using System.Text;
    using System.Threading.Tasks;

    [TestClass]
    public class CreateCategoryShould
    {
        [TestMethod]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow(null)]

        public async Task ThrowArgumentNullException_WhenArgumentsAreEmptyWhiteSpacesOrNull(string categoryName)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var sut = new CategoryService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.CreateCategoryAsync(categoryName));
        }

        [TestMethod]
        [DataRow("ValidCategory")]
        public async Task CreateCategory_WhenCategoryExists(string categoryName)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = categoryName.ToUpper(),
                },
                new Category()
                {
                    Name = "TestCategory",
                }
            };

            repositoryMock.Setup(c => c.All<Category>()).Returns(categories.AsQueryable().BuildMock());

            var sut = new CategoryService(repositoryMock.Object, validatorServiceStub);

            // Act
            var categoryCreateSut = await sut.CreateCategoryAsync(categoryName);

            // Assert
            Assert.AreEqual(categoryCreateSut, categoryName.ToUpper());
        }

        [TestMethod]
        [DataRow("ValidCategory")]
        public async Task CreateCategory_WhenCategoryNotExists(string categoryName)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Vali Category",
                },
                new Category()
                {
                    Name = "TestCategory",
                }
            };

            repositoryMock.Setup(c => c.All<Category>()).Returns(categories.AsQueryable().BuildMock());

            var sut = new CategoryService(repositoryMock.Object, validatorServiceStub);

            // Act
            var categoryCreateSut = await sut.CreateCategoryAsync(categoryName);

            // Assert
            Assert.AreEqual(categoryCreateSut, categoryName.ToUpper());
            repositoryMock.Verify(c => c.SaveAsync(), Times.Once());
        }

        [TestMethod]
        [DataRow("SE")]
        [DataRow("Category name must be between 3 and 20 characters!")]
        public async Task ThrowArgumentException_WhenArgumentIsIncorrect(string categoryName)
        {
            // Arrange
            var repositoryStub = new Mock<IRepository>();
            var validatorServiceStub = new ValidatorService();

            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Vali Category",
                },
                new Category()
                {
                    Name = "TestCategory",
                }
            };

            repositoryStub.Setup(c => c.All<Category>()).Returns(categories.AsQueryable().BuildMock());

            var sut = new CategoryService(repositoryStub.Object, validatorServiceStub);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.CreateCategoryAsync(categoryName));
        }
    }
}
