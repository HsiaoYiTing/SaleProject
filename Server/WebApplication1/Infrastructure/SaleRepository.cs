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

    public async Task<int> AddSaleAsync(List<Sale> saleList)
    {
        using var connection = CreateConnection();

        await connection.OpenAsync();

        // Copy 到 sale
        using (var writer = connection.BeginBinaryImport("""
            COPY sale
            (
                id,
                store_id,
                product_id,
                price,
                qty,
                sale_time,
                create_time,
                update_by
            )
            FROM STDIN (FORMAT BINARY)
            """))
        {
            foreach (var sale in saleList)
            {
                writer.StartRow();

                writer.Write(sale.Id, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(sale.Store.Id, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(sale.Product.Id, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(sale.Price, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(sale.Qty, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(sale.SaleTime, NpgsqlTypes.NpgsqlDbType.Timestamp);
                writer.Write(sale.CreateTime, NpgsqlTypes.NpgsqlDbType.Timestamp);
                writer.Write(sale.UpdateBy, NpgsqlTypes.NpgsqlDbType.Varchar);
            }

            await writer.CompleteAsync();
        } 
      
        return saleList.Count;
    }

    public async Task<int> DeleteSaleAsync(DateTime startTime, DateTime endTime, string updateBy)
    {
        const string sql = """
            DELETE FROM sale WHERE create_time >= @startTime AND create_time < @endTime AND update_by = @updateBy;
        """;

        using var connection = CreateConnection();

        return await connection.ExecuteAsync(sql, new
        {
            startTime,
            endTime,
            updateBy
        });
    }

    public async Task<List<Sale>?> GetSalesByConditionsAsync(DateTime startTime, DateTime endTime, string storeId)
    {
        string sql = """
            SELECT 
            sale.id, sale.store_id, sale.product_id, sale.price, sale.qty, 
            sale.sale_time AS SaleTime, 
            sale.create_time AS CreateTime, 
            sale.update_by AS UpdateBy ,

            s.id, s.name, s.address, 

            p.id, p.name, p.price

            FROM sale
            LEFT JOIN product p ON sale.product_id = p.id
            LEFT JOIN store s ON sale.store_id = s.id
            WHERE sale_time >= @startTime AND sale_time < @endTime AND store_Id = @storeId;
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