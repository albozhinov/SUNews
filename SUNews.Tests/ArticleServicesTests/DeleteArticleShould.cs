using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SUNews.Data.Repository;
using SUNews.Services.Providers;
using SUNews.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUNews.Tests.ArticleServicesTests
{
    [TestClass]
    public class DeleteArticleShould
    {
		[DataTestMethod]
		[DataRow(null)]
		[DataRow("")]
		public async Task ThrowArgumentException_WhenArgumentsAreNull(string articleId)
		{
			// Arrange
			var repositoryStub = new Mock<IRepository>();
			var validatorServiceStub = new Mock<IValidatorService>();

			var sut = new ArticleService(repositoryStub.Object, validatorServiceStub.Object);

			// Act & Assert
			await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.DeleteArticleAsync(articleId));
		}

		[DataTestMethod]
		[DataRow("Not-valid-GUID")]
		public async Task ThrowArgumentException_WhenArgumentsAreIncorrect(string articleId)
		{
			// Arrange
			var repositoryStub = new Mock<IRepository>();
			var validatorServiceStub = new Mock<IValidatorService>();

			var sut = new ArticleService(repositoryStub.Object, validatorServiceStub.Object);

			// Act & Assert
			await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.DeleteArticleAsync(articleId));
		}

	}
}
