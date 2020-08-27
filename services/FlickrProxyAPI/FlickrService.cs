using System;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace FlickrProxyAPI
{
    public interface IFlickrService
    {
        Task<string> GetFeedContentAsync(Uri url);
    }

    public class FlickrService : IFlickrService
    {
        private readonly IHttpClientFactory _clientFactory;

        public FlickrService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> GetFeedContentAsync(Uri url)
        {
            var client = _clientFactory.CreateClient();

            var feedResponse = await client.GetAsync(url);

            feedResponse.EnsureSuccessStatusCode();

            return await feedResponse.Content.ReadAsStringAsync();
        }
    }
}