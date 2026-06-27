public interface ISaleSumRepository
{
    Task<SaleSum?> AddSaleAsync(SaleSum sum);
    Task<SaleSum?> UpdateSaleAsync(SaleSum sum);
    Task<List<SaleSum>?> GetSalesByConditionsAsync(DateTime startTime, DateTime endTime, string storeId);
}