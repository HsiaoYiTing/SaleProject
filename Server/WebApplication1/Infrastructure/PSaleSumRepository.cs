using Dapper;
using Npgsql;

public class PSaleSumRepository : BaseRepository, ISaleSumRepository
{
    public PSaleSumRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<SaleSum?> AddSaleAsync(SaleSum sum)
    {
        const string sql = """
            CALL add_sale_sum(
                @id,
                @storeId,
                @saleTime,
                @price
            );
        """;

        try {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", sum.Id);
            command.Parameters.AddWithValue("@storeId", sum.Store_Id);
            command.Parameters.AddWithValue("@saleTime", sum.Sale_Time);
            command.Parameters.AddWithValue("@price", sum.Price);

            await command.ExecuteNonQueryAsync();
            return sum;
        }
        catch (PostgresException ex)
        {
            Console.WriteLine($"PostgreSQL 錯誤代碼：{ex.SqlState}");
            Console.WriteLine($"錯誤訊息：{ex.MessageText}");
            Console.WriteLine($"詳細內容：{ex.Detail}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"執行失敗：{ex.Message}");
            return null;

        }
    }

    public async Task<List<SaleSum>?> GetSalesByConditionsAsync(DateTime startTime, DateTime endTime, string storeId)
    {
        string sql = """
            SELECT * FROM get_sale_by_condition(
                    @startTime,
                    @endTime,
                    @storeId
                );
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
            CALL update_sale_sum(
                @id,
                @storeId,
                @saleTime,
                @price,
                @nowTime
            );
        """;

        try {

            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", sum.Id);
            command.Parameters.AddWithValue("@storeId", sum.Store_Id);
            command.Parameters.AddWithValue("@saleTime", sum.Sale_Time);
            command.Parameters.AddWithValue("@price", sum.Price);
            command.Parameters.AddWithValue("@nowTime", DateTime.Now);

            await command.ExecuteNonQueryAsync();
            return sum;
        }
        catch (PostgresException ex)
        {
            Console.WriteLine($"PostgreSQL 錯誤代碼：{ex.SqlState}");
            Console.WriteLine($"錯誤訊息：{ex.MessageText}");
            Console.WriteLine($"詳細內容：{ex.Detail}");
            return null;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"執行失敗：{ex.Message}");
            return null;

        }
    }
}