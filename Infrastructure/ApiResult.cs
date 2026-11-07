namespace GhasemWebServiceRestFull.Dependency
{

    public class ApiResult<T>
    {
        public bool success { get; set; }

        public short StatusCode { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        public ApiError? Error { get; set; }

        public static ApiResult<T> Success(T? data)
        {
            return new ApiResult<T>()
            {
                success = true,
                StatusCode = 200,
                Data = data,
            };
        }
        public static ApiResult<T> Failed(T? data,Exception? ex)
        {
            
            return new ApiResult<T>()
            {
                Data = data,
                success = false,
                Error = new ApiError(ex.Message,ex.InnerException?.Message),
                Message = ex.Message,
                StatusCode = 500
                
            };
        }
    }


}
