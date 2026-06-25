
using Dapper;
using Npgsql;

public class SaleSumRepository : ISaleSumRepository
{
    private readonly IConfiguration _configuration;

    public SaleSumRepository(IConfiguration configuration)
    {
        _configuration = configuration;
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

    public async Task<List<SaleSum>?> GetSalesByConditionsAsync(DateOnly? startDate, DateOnly? endDate, string storeId)
    {
        
        string sql = """
            SELECT * FROM sale_sum  
        """;

        var hasCondition = false;
        if (!string.IsNullOrWhiteSpace(storeId))
        {
            sql += """ WHERE store_id = @storeId """;
            hasCondition = true;
        }

        if (startDate != null)
        {
            sql += hasCondition ? " AND " : " WHERE " ;

            sql += """
                sale_time >= @startDate
            """;

            hasCondition = true;
        }

        if (endDate != null)
        {
            sql += hasCondition ? " AND " : " WHERE " ;

            sql += """
                sale_time <= @endDate
            """;

            hasCondition = true;
        }

        using var connection = CreateConnection();
        
        var result = await connection.QueryAsync<SaleSum>(sql, new
        {
            storeId,
            startDate = startDate?.ToDateTime(TimeOnly.MinValue),
            endDate = endDate?.ToDateTime(TimeOnly.MinValue)
        });

        return result.ToList();
    }

    private NpgsqlConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        return new NpgsqlConnection(connectionString);
    }
}