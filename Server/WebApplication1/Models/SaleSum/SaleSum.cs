public class SaleSum
{
    public required string Id { get; set; }
    public required string Store_Id { get; set; }
    public required decimal Price { get; set; }
    public required DateOnly Sale_Time { get; set; }
    public required DateTime Create_Time { get; set; }
}