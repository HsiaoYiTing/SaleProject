public class ResponseFactory
{
    private static int SuccessCode = 200;
    private static int ErrorCode = 500;

    public static ResponseBase<T> CreateSuccessResponse<T>(T data, string message = "Success")
    {
        return new ResponseBase<T>
        {
            Code = SuccessCode,
            Message = message,
            Data = data
        };
    }

     public static ResponseBase<T> CreateErrorResponse<T>(T data, string message = "Error")
    {
        return new ResponseBase<T>
        {
            Code = ErrorCode,
            Message = message,
            Data = data
        };
    }

     public static ResponseBase CreateSuccessResponse(string message = "Success")
    {
        return new ResponseBase
        {
            Code = SuccessCode,
            Message = message
        };
    }

    public static ResponseBase CreateErrorResponse(string message = "Error")
    {
        return new ResponseBase
        {
            Code = ErrorCode,
            Message = message
        };
    }

    internal static ResponseBase<T> CreateErrorResponse<T>(string v)
    {
        throw new NotImplementedException();
    }
}