using AutoMapper;
using ProductsApp.Models;
using ProductsApp.ViewModels;

namespace ProductsApp.Helpers
{
    public class ProductMapper : Profile
    {
        public ProductMapper() {

            CreateMap<ProductResponse, Product>();
        }  
    }

}
