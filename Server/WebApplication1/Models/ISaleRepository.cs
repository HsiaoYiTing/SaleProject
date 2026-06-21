public interface ISaleRepository
{
    Task<SaleRecord?> AddSale(SaleRecord record);
    Task<List<SaleRecord>?> GetSalesByDate(DateOnly date);
}