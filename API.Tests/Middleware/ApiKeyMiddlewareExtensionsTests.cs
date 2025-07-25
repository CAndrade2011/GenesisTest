using API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace API.Tests.Middleware
{
    public class ApiKeyMiddlewareExtensionsTests
    {
        [Fact]
        public void UseApiKeyValidation_DeveAdicionarMiddlewareSemExcecao()
        {
            // Arrange
            var appBuilderMock = new Mock<IApplicationBuilder>();
            appBuilderMock.Setup(x => x.Use(It.IsAny<Func<RequestDelegate, RequestDelegate>>()))
                          .Returns(appBuilderMock.Object);

            // Act
            var result = ApiKeyMiddlewareExtensions.UseApiKeyValidation(appBuilderMock.Object);

            // Assert
            Assert.Equal(appBuilderMock.Object, result);
        }
    }
} 