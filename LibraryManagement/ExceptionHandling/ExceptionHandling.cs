namespace LibraryManagement.ExceptionHandling
{
    public class ProblemDetails
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int Status { get; set; }
        public string? Detail { get; set; }
        public string? Instance { get; set; }
        public Dictionary<string, string[]> Extensions { get; set; } = new Dictionary<string, string[]>();
    }

    public class ServiceResult
    {
        public ServiceResultStatus Status { get; set; }
        public ProblemDetails? ProblemDetails { get; set; }
    }

    public enum ServiceResultStatus
    {
        Success = 1,
        Error = 2
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T>
            {
                Data = data,
                Status = ServiceResultStatus.Success
            };
        }

        public static ServiceResult<T> Fail(string message, int status = 500)
        {
            return new ServiceResult<T>
            {
                Status = ServiceResultStatus.Error,
                ProblemDetails = new ProblemDetails
                {
                    Status = status,
                    Detail = message
                }
            };
        }
    }

}