
using Dapper;
using Npgsql;

public class SaleSumRepository : BaseRepository, ISaleSumRepository
{
    public SaleSumRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<SaleSum?> AddSaleAsync(SaleSum sum)
    {

        string sql = """
            INSERT INTO sale_sum (id, store_id, sale_time, price) 
            VALUES(@id, @store_id, @sale_time, @price) 
            RETURNING id;
        """;

        using var connection = CreateConnection();
        await connection.OpenAsync();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", sum.Id);
        command.Parameters.AddWithValue("@store_id", sum.Store_Id);
        command.Parameters.AddWithValue("@sale_time", sum.Sale_Time);
        command.Parameters.AddWithValue("@price", sum.Price);

        var rowsAffected = await command.ExecuteNonQueryAsync();
        if (rowsAffected >= 1 )
        {
            return sum;
        } 
        else
        {
            return null;
        }
    }

    public async Task<List<SaleSum>?> GetSalesByConditionsAsync(DateTime startTime, DateTime endTime, string storeId)
    {
        
        string sql = """
            SELECT * FROM sale_sum WHERE store_id = @storeId 
            AND sale_time >= @startTime AND sale_time < @endTime
        """;

        using var connection = CreateConnection();
        var result = await connection.QueryAsync<SaleSum>(sql, new
        {
            storeId,
            startTime,
            endTime
        });

        return result.ToList();
    }

    public async Task<SaleSum?> UpdateSaleAsync(SaleSum sum)
    {

         string sql = """
            INSERT INTO sale_sum (id, store_id, sale_time, price )
            VALUES (@id, @store_id, @sale_time, @price)
            ON CONFLICT (id)
            DO UPDATE
            SET
                price = @price, create_time = @nowTime
            RETURNING id;
        """;

        using var connection = CreateConnection();
        await connection.OpenAsync();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", sum.Id);
        command.Parameters.AddWithValue("@store_id", sum.Store_Id);
        command.Parameters.AddWithValue("@sale_time", sum.Sale_Time);
        command.Parameters.AddWithValue("@price", sum.Price);
        command.Parameters.AddWithValue("@nowTime", DateTime.Now);

        var rowsAffected = await command.ExecuteNonQueryAsync();
        if (rowsAffected >= 1 )
        {
            return sum;
        } 
        else
        {
            return null;
        }
    }
}