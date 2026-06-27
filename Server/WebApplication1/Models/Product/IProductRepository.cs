public interface IProductRepository
{
    Task<Product?> GetProductByNameAsync(string name);

    Task<List<Product>> GetAllAsync();
}