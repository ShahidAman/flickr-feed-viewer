using System;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FlickrProxyAPI.Controllers
{
    [ApiController]
    [Route("feeds")]
    public class FlickrProxyController : ControllerBase
    {
        public FlickrProxyController(IFlickrService flickrService)
        {
            _flickrService = flickrService;
        }

        private const string flickrUrl = "https://www.flickr.com/services/feeds/photos_public.gne?format=json&nojsoncallback=1";

        private readonly IFlickrService _flickrService;

        [HttpGet]
        public async Task<string> GetAsync()
        {            
            return await _flickrService.GetFeedContentAsync(new Uri(flickrUrl));
        }

        [HttpGet]
        [Route("search/")]
        public async Task<string> SearchAsync(string tags)
        {
            var urlString = flickrUrl+ "&tags="+tags;
            var url = new Uri(urlString);
            return await _flickrService.GetFeedContentAsync(url);
        }
    }
}
