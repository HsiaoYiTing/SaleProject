public class SaleRecord
{
    public string Id { get; set; }

    public string StoreId { get; set; }

    public string ProductId { get; set; }

    public int Amount { get; set; }

    public DateTime SaleTime { get; set; }

    public DateTime CreateTime { get; set; } 

    public string UpdateBy { get; set; } 
}