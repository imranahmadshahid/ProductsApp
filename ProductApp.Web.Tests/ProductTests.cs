using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsApp.Controllers;
using ProductsApp.Helpers;
using ProductsApp.Implementation;
using ProductsApp.Infrastructure;
using ProductsApp.Models;
using ProductsApp.ViewModels;

namespace ProductApp.Web.Tests
{
    public class ProductTests
    {
        Mock<IHttpClientHelper> httpClientHelper;
        Mock<ILogger<HomeController>> homelogger;
        Mock<ILogger<ProductApiFacade>> productLogger;
        IConfiguration configuration;
        IMapper mapper;

        [SetUp]
        public void Setup()
        {
            httpClientHelper = new Mock<IHttpClientHelper>();
            homelogger = new Mock<ILogger<HomeController>>();
            productLogger = new Mock<ILogger<ProductApiFacade>>();

            httpClientHelper.Setup(c => c.GetAsync<RootProduct>(It.Is<Uri>(x=> x.ToString().Equals("https://dummyjson.com/products")))).ReturnsAsync(new RootProduct()
            {
                Limit = 10,
                Products = new List<ProductResponse>() {
                new ProductResponse{Brand="Apple",Rating = 4.98,Title ="Iphone 14 pro", Price =1500,DiscountPercentage = 20},
                new ProductResponse{Brand="Samsung",Rating = 4.96,Title ="S 22 ultra", Price =1400,DiscountPercentage = 12},
                new ProductResponse{Brand="OnePlus",Rating = 4,Title ="Oneplus 8", Price = 900,DiscountPercentage = 5},
                new ProductResponse{Brand="Huawei",Rating = 4.3,Title ="Huawei Mate 50", Price =1100,DiscountPercentage = 25},
                new ProductResponse{Brand="Google",Rating = 4.2,Title ="Google Pixel 7", Price =700,DiscountPercentage = 12},
            }
            });

            var myConfiguration = new Dictionary<string, string> { { "BaseUrl", "https://dummyjson.com" } };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMapper());
            });

            mapper = mockMapper.CreateMapper();
        }


        [Test]
        public async Task ProductController_Index_ReturnAsViewResult_WithSuccessProductResponse()
        {
            var productFacade = new ProductApiFacade(configuration, httpClientHelper.Object, mapper, productLogger.Object);

            var controller = new HomeController(homelogger.Object, productFacade);

            var response = await controller.Index();

            var controllerResponse = response as ViewResult;

            var model = controllerResponse.Model as ProductViewModel;

            Assert.IsNotNull(model);
            Assert.IsTrue(model.Products.Count == 4);
            Assert.IsTrue(model.TrendingBrands.Any(x => x.Equals("Apple")));
            Assert.IsTrue(model.TrendingBrands.Any(x => x.Equals("Samsung")));

        }
        [Test]
        public async Task ProductController_Index_ReturnAsViewResult_WithEmptyProductResponse()
        {
            httpClientHelper.Setup(c => c.GetAsync<RootProduct>(It.Is<Uri>(x => x.ToString().Equals("https://dummyjson.com/products")))).ReturnsAsync(new RootProduct());

            var productFacade = new ProductApiFacade(configuration, httpClientHelper.Object, mapper, productLogger.Object);

            var controller = new HomeController(homelogger.Object, productFacade);

            var response = await controller.Index();

            var controllerResponse = response as ViewResult;

            var model = controllerResponse.Model as ProductViewModel;

            Assert.IsTrue(model == null);

        }


        [Test]
        public async Task ProductController_Index_ReturnAsViewResult_WithInvalidUrl()
        {

            var myConfiguration = new Dictionary<string, string> { { "BaseUrl", "" } };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var productFacade = new ProductApiFacade(configuration, httpClientHelper.Object, mapper, productLogger.Object);

            var controller = new HomeController(homelogger.Object, productFacade);

            var response = await controller.Index();

            var controllerResponse = response as ViewResult;

            var model = controllerResponse.Model as ProductViewModel;

            Assert.IsTrue(model == null);
        }
    }
}