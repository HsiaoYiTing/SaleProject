public class AddSaleRequest
{
    public String Guid { get; set; }
    public String Store { get; set; }
    public String SaleTime { get; set; }
    public decimal SalePrice { get; set; }
    public String ItemName { get; set; }
    public int Qty { get; set; }
}