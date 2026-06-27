public class Sale
{
    public required string Id { get; set; }

    public required Store Store { get; set; }

    public required Product Product { get; set; }

    public required decimal Price { get; set; }

    public required int Qty { get; set; }

    public required DateTime SaleTime { get; set; }

    public required DateTime CreateTime { get; set; } 

    public required string UpdateBy { get; set; } 
}