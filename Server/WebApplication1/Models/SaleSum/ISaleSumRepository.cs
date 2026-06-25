public interface ISaleSumRepository
{
    Task<SaleSum?> AddSaleAsync(SaleSum sum);
    Task<SaleSum?> UpdateSaleAsync(SaleSum sum);
    Task<List<SaleSum>?> GetSalesByConditionsAsync(DateOnly? startDate, DateOnly? endDate, string storeId);
}