public class AddSaleRequest
{
    public required string Guid { get; set; }
    public required string Store { get; set; }
    public required string SaleTime { get; set; }
    public required decimal SalePrice { get; set; }
    public required string ItemId { get; set; }
    public required string ItemName { get; set; }
    public required int Qty { get; set; }
    public required string UpdateBy { get; set; }
}