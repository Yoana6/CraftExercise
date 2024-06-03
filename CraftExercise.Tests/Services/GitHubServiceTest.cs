using System.Threading.Tasks;
using Moq;
using Xunit;
using System.Net.Http;
using System.Net;
using Moq.Protected;
using System.Threading;
using System;
using CraftExercise.Models;
using CraftExercise.Services;

namespace GitHubFreshdeskIntegration.Tests.Services
{
    public class GitHubServiceTests
    {
        [Fact]
        public async Task GetGitHubUser_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var username = "Yoana6";
            var expectedUser = new GitHubUser { Login = username, Name = "Yoana6", Email = "y.y.konstantinova@gmail.com" };

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
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"login\": \"Yoana6\", \"name\": \"Yoana6\", \"email\": \"y.y.konstantinova@gmail.com\"}")
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.github.com/")
            };

            var service = new GitHubService(httpClient);

            // Act
            var result = await service.GetGitHubUser(username);

            // Assert
            Assert.Equal(expectedUser.Login, result.Login);
            Assert.Equal(expectedUser.Name, result.Name);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public async Task GetGitHubUser_ThrowsException_WhenUserDoesNotExist()
        {
            // Arrange
            var username = "nonexistentuser";

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
                    StatusCode = HttpStatusCode.NotFound
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.github.com/")
            };

            var service = new GitHubService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetGitHubUser(username));
        }
    }
}