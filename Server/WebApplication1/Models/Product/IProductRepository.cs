public interface IProductRepository
{
    Task<Product?> GetProductByNameAsync(string name);
}