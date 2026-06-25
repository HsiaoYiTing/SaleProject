using Dapper;
using Npgsql;

public class SaleRepository : BaseRepository, ISaleRepository
{
    public SaleRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<int> AddSaleAsync(Sale record)
    {
        string sql = """
            INSERT INTO sale (id, store_id, product_id, price, sale_time, qty, update_by) 
            VALUES(@id, @store_id, @product_id, @price, @sale_time, @qty, @update_by);
        """;

        using var connection = CreateConnection();
        await connection.OpenAsync();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", record.Id);
        command.Parameters.AddWithValue("@store_id", record.Store.Id);
        command.Parameters.AddWithValue("@product_id", record.Product.Id);
        command.Parameters.AddWithValue("@price", record.Price);
        command.Parameters.AddWithValue("@sale_time", record.SaleTime);
        command.Parameters.AddWithValue("@qty", record.Qty);
        command.Parameters.AddWithValue("@update_by", record.UpdateBy);

        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected;
    }

    public async Task<int> AddSaleAsync(List<SaleLine> saleList)
    {
        using var connection = CreateConnection();

        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        // Copy 到 sale_temp
        using (var writer = connection.BeginBinaryImport("""
            COPY sale_temp
            (
                id,
                store_id,
                product_name,
                price,
                sale_time,
                qty,
                update_by
            )
            FROM STDIN (FORMAT BINARY)
            """))
        {
            foreach (var sale in saleList)
            {
                writer.StartRow();

                writer.Write(sale.Id, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(sale.Store, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(sale.ProductName, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(sale.Price, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(sale.SaleTime, NpgsqlTypes.NpgsqlDbType.Timestamp);
                writer.Write(sale.Qty, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(sale.UpdateBy, NpgsqlTypes.NpgsqlDbType.Varchar);
            }

            await writer.CompleteAsync();
        } 

        // 寫入 sale
        const string insertSql = """

            INSERT INTO sale(id, store_id, product_id, price, sale_time, qty, update_by)
            SELECT t.id, t.store_id, p.id, t.price, t.sale_time, t.qty, t.update_by
            FROM sale_temp t
            INNER JOIN product p
                ON p.name = t.product_name;
            """;

        using (var command = new NpgsqlCommand(insertSql, connection, transaction))
        {
            await command.ExecuteNonQueryAsync();
        }

        // 清空 sale_temp
        using (var command = new NpgsqlCommand("TRUNCATE TABLE sale_temp;", connection, transaction))
        {
            await command.ExecuteNonQueryAsync();
        }
        await transaction.CommitAsync();


        return saleList.Count;
    }

    public async Task<List<Sale>?> GetSalesByConditionsAsync(DateTime startTime, DateTime endTime, string storeId)
    {
        string sql = """
            SELECT * FROM sale
            LEFT JOIN product p ON sale.product_id = p.id
            LEFT JOIN store s ON sale.store_id = s.id
            WHERE sale_time >= @startTime AND sale_time <= @endTime AND store_Id = @storeId;
        """;
        
        using var connection = CreateConnection();
        
        var records = await connection.QueryAsync<Sale, Store, Product, Sale>(sql, (sale, store, product) =>
        {
            sale.Store = store;
            sale.Product = product;

            return sale;
        }, 
        new
        {
            startTime,
            endTime,
            storeId
        });

        return records.ToList();
    }
}