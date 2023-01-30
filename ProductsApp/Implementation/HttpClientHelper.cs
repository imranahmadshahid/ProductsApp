using Microsoft.Net.Http.Headers;
using ProductsApp.Infrastructure;
using System.Text.Json;

namespace ProductsApp.Implementation
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpClientHelper> _logger;
        public HttpClientHelper(IHttpClientFactory httpClientFactory, ILogger<HttpClientHelper> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<T> GetAsync<T>(Uri uri) where T : class
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                var httpClient = _httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync();

                    var response = await JsonSerializer.DeserializeAsync<T>(contentStream);
                    return response;
                }
                else
                {
                    _logger.LogWarning($"Unable to get the data from {uri}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occcured while getting the data from : {uri} : " + ex.Message);
            }
            return default;
        }
    }
}
