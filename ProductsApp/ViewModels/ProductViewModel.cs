
namespace ProductsApp.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }

        public List<string> TrendingBrands { get; set; }
    }

    public class Product{
        public string Title { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }

    }


}
