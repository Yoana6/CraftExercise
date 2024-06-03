
using Moq;
using System.Net;
using Moq.Protected;
using CraftExercise.Models;
using CraftExercise.Services;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace CraftExercise.Tests.Services
{
    public class FreshdeskServiceTests
    {
        [Fact]
        public async Task CreateOrUpdateContact_ReturnsTrue_WhenSuccessful()
        {
            // Arrange
            var contact = new FreshdeskContact { Name = "Test User", Email = "testuser@example.com" };
            var subdomain = "testdomain";
            var apiKey = "testapikey";

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);

            var service = new FreshdeskService(httpClient);

            // Act
            var result = await service.CreateOrUpdateContact(contact, subdomain, apiKey);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateOrUpdateContact_ThrowsException_WhenFailed()
        {
            // Arrange
            var contact = new FreshdeskContact { Name = "Test User", Email = "testuser@example.com" };
            var subdomain = "testdomain";
            var apiKey = "testapikey";

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);

            var service = new FreshdeskService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.CreateOrUpdateContact(contact, subdomain, apiKey));
        }
    }
}