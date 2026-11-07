namespace GhasemWebServiceRestFull.Dependency
{
    public class ApiError
    {
        public ApiError(string? message, object? details)
        {
            Message = message;
            Details = details;
        }
        public string? Message { get; set; }
        public object? Details { get; set; }
    }
}
