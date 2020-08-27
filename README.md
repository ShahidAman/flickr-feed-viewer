# flickr-feed-viewer
Application built using ReactJS and .NET Core.
The ui displays images from Flickr [Public feed](https://www.flickr.com/services/feeds/ )

### Things to consider for making this *production* ready
1. Move out URIs etc to config / env
2. Secure .NET service , authN and authZ using ASP.NET Core Identity or azureAD or put it behind an API gateway + load balancing
3. Configure CORS
4. Configure / enhance logging /monitoring (add application insights?)
