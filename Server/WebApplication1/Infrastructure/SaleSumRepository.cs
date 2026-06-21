
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
        throw new NotImplementedException();
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
                sale_date >= @startDate
            """;

            hasCondition = true;
        }

        if (endDate != null)
        {
            sql += hasCondition ? " AND " : " WHERE " ;

            sql += """
                sale_date <= @endDate
            """;

            hasCondition = true;
        }

        using var connection = CreateConnection();
        
        var users = await connection.QueryAsync<SaleSum>(sql, new
        {
            storeId,
            startDate = startDate?.ToDateTime(TimeOnly.MinValue),
            endDate = endDate?.ToDateTime(TimeOnly.MinValue)
        });

        return users.ToList();
    }

    private NpgsqlConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        return new NpgsqlConnection(connectionString);
    }
}