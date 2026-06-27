public interface ISaleRepository
{
    Task<int> AddSaleAsync(Sale sale);

    Task<int> AddSaleAsync(List<Sale> saleList);

    Task<int> DeleteSaleAsync(DateTime startTime, DateTime endTime, string updateBy);
    
    Task<List<Sale>?> GetSalesByConditionsAsync(DateTime startTime, DateTime endTime, string storeId);
}