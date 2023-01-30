using ProductsApp.ViewModels;

namespace ProductsApp.Infrastructure
{
    public interface IProductFacade
    {
        public Task<ProductViewModel> GetProducts();
    }
}
