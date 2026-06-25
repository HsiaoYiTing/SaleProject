using Dapper;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Product?> GetProductByNameAsync(string name)
    {
         const string sql = """
            SELECT * FROM product 
            WHERE name = @name;
        """;

        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Product>(
            sql,
            new { name }
        );
    }
}