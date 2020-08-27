using Xunit;
using System.Threading.Tasks;
using Moq;
using FlickrProxyAPI.Controllers;
using System.Text.Json;
using System;

namespace FlickrProxyAPI.Tests
{
    public class FlickrProxyControllerTests
    {
        private Mock<IFlickrService> flickrServiceMock;

        public FlickrProxyControllerTests()
        {
            flickrServiceMock = new Mock<IFlickrService>();
            flickrServiceMock.Setup(
                fs => fs.GetFeedContentAsync(It.IsAny<Uri>()))
                            .Returns(Task.FromResult("{\r\n    \"title\": \"foo\",\r\n    \"items\": []\r\n}"));
        }

        [Fact]
        public async void Get_Should_Return_FeedContent()
        {
            var target = new FlickrProxyController(flickrServiceMock.Object);
            var feed = await target.GetAsync();
            var actual = JsonDocument.Parse(feed).RootElement.GetProperty("title").GetString();
            Assert.Equal("foo", actual);
        }

        [Fact]
        public async void Search_Should_Return_FeedContent()
        {
            var target = new FlickrProxyController(flickrServiceMock.Object);

            flickrServiceMock.Setup(
                fs => fs.GetFeedContentAsync(It.IsAny<Uri>()))
                            .Returns(Task.FromResult("{\r\n    \"title\": \"feed containing cats\",\r\n    \"items\": []\r\n}"));

            var feed = await target.SearchAsync("cats");
            var actual = JsonDocument.Parse(feed).RootElement.GetProperty("title").GetString();
            Assert.Contains("cats", actual);
        }
    }
}