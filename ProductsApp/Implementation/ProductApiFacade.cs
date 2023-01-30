using AutoMapper;
using ProductsApp.Constants;
using ProductsApp.Infrastructure;
using ProductsApp.Models;
using ProductsApp.ViewModels;
using System.Net.Http;

namespace ProductsApp.Implementation
{
    public class ProductApiFacade : IProductFacade
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientHelper _httpClient;
        private readonly ILogger<ProductApiFacade> _logger;
        private readonly IMapper _mapper;
        private readonly string _baseUrl;
        public ProductApiFacade(IConfiguration configuration, IHttpClientHelper httpClient, IMapper mapper, ILogger<ProductApiFacade> logger)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _mapper = mapper;
            _logger = logger;
            _baseUrl = _configuration.GetValue<string>("BaseUrl");
        }

        public async Task<ProductViewModel> GetProducts()
        {
            try
            {
                var uri = new Uri($"{_baseUrl}/{ProductApiConstants.GetProduct}");
                var apiResponse = await _httpClient.GetAsync<RootProduct>(uri);

                if (apiResponse != null)
                {
                    var mappedProducts = _mapper.Map<List<Product>>(apiResponse.Products);

                    var response = new ProductViewModel
                    {
                        Products = mappedProducts,
                        TrendingBrands = apiResponse.Products.Where(x => x.Rating > 4.8).Select(x => x.Title).ToList()
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while getting the product : " + ex.Message);
            }
            return default;
        }
    }
}
