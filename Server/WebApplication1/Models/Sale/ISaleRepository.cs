public interface ISaleRepository
{
    Task<int> AddSaleAsync(Sale sale);

    Task<int> AddSaleAsync(List<Sale> saleList);
    Task<List<Sale>?> GetSalesByDateAsync(DateTime startTime, DateTime endTime, string storeId);
}