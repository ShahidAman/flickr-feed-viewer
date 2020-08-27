using Xunit;
using Moq;
using System.Net.Http;
using System.Net;
using System;

namespace FlickrProxyAPI.Tests
{
    public class FlickrServiceTests
    {
        private Mock<IHttpClientFactory> clientFactoryMock;

        public FlickrServiceTests()
        {
            var httpClient = new HttpClient(new DelegatingHandlerStub(HttpStatusCode.OK));
            clientFactoryMock = new Mock<IHttpClientFactory>(MockBehavior.Loose);
            clientFactoryMock.Setup(cf => cf.CreateClient(It.IsAny<string>())).Returns(httpClient);
        }

        [Fact]
        public async void GetFeedContent_OnSuccess_Should_ReturnContent()
        {
            var target = new FlickrService(clientFactoryMock.Object);
            var content = await target.GetFeedContentAsync(new Uri("http://foo/"));

            Assert.NotNull(content);
        }

        [Fact]
        public async void GetFeedContent_OnError_Should_ThrowException()
        {
            var httpClient = new HttpClient(new DelegatingHandlerStub(HttpStatusCode.NotFound));
            clientFactoryMock = new Mock<IHttpClientFactory>(MockBehavior.Strict);
            clientFactoryMock.Setup(cf => cf.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var target = new FlickrService(clientFactoryMock.Object);

            var ex = await Assert.ThrowsAsync<HttpRequestException>(() => target.GetFeedContentAsync(new Uri("http://foo/")));
            Assert.IsAssignableFrom<HttpRequestException>(ex);
        }
    }
}
