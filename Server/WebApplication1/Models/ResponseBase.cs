public class ResponseBase<T>
{
    public int Code { get; set; }
    public string Message { get; set; } = "";

    public T? Data { get; set; }
}

public class ResponseBase
{
    public int Code { get; set; }
    public string Message { get; set; } = "";
}
