public class Sale
{
    public string Id { get; set; }

    public Store Store { get; set; }

    public Product Product { get; set; }

    public decimal Price { get; set; }

    public int Qty { get; set; }

    public DateTime SaleTime { get; set; }

    public DateTime CreateTime { get; set; } 

    public string UpdateBy { get; set; } 
}