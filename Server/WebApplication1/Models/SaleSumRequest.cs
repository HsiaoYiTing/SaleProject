public class SaleSumRequest
{
    public string StoreId{ get; set;}
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}