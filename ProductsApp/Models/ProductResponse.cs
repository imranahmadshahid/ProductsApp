using System.Text.Json.Serialization;

namespace ProductsApp.Models
{
    public class ProductResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("price")]
        public int Price { get; set; }
        [JsonPropertyName("discountPercentage")]
        public double DiscountPercentage { get; set; }
        [JsonPropertyName("rating")]
        public double Rating { get; set; }
        [JsonPropertyName("stock")]
        public int Stock { get; set; }
        [JsonPropertyName("brand")]
        public string Brand { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }
        [JsonPropertyName("images")]
        public List<string> Images { get; set; }
    }

    public class RootProduct
    {
        [JsonPropertyName("products")]
        public List<ProductResponse> Products { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
        [JsonPropertyName("skip")]
        public int Skip { get; set; }
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    }
}
