namespace SUNews.Tests.Services.ArticleServiceTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SUNews.Data.Context;
    using SUNews.Data.Repository;
    using SUNews.Services.Providers;
    using SUNews.Services.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestClass]
    public class CreateArticleShould
    {
        [DataTestMethod]
        [DataRow(null, "Valid Content", "Valid Url", "Valid AuthorName")]
        public async Task ThrowArgumentException_WhenArgumentsAreNull(string title,
                                                                            string content,
                                                                            string imageUrl,
                                                                            string authorName)
        {
            // Arrange
            var dbContextStub = new Mock<IRepository>();
            var validatorServiceProviderMock = new Mock<IValidatorService>();

            var sut = new ArticleService(dbContextStub.Object, validatorServiceProviderMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.CreateArticleAsync(title,
                                                                                                              content,
                                                                                                              imageUrl,
                                                                                                              authorName,
                                                                                                              new List<string>()));
        }



    }

}
