using Dapper;

public class StoreRepository : BaseRepository, IStoreRepository
{
    public StoreRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Store?> GetStoreByIdAsync(string id)
    {
          const string sql = """
            SELECT * FROM store 
            WHERE id = @id;
        """;

        using var connection = CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Store>(
            sql,
            new { id }
        );
    }
}