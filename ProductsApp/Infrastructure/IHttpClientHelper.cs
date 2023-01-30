namespace ProductsApp.Infrastructure
{
    public interface IHttpClientHelper
    {
        Task<T> GetAsync<T>(Uri uri) where T : class;
    }
}
