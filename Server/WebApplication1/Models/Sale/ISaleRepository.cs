public interface ISaleRepository
{
    Task<int> AddSaleAsync(Sale sale);

    Task<int> AddSaleAsync(List<SaleLine> saleList);
    
    Task<List<Sale>?> GetSalesByConditionsAsync(DateTime startTime, DateTime endTime, string storeId);
}