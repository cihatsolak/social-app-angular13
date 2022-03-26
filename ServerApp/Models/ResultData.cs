namespace ServerApp.Models
{
    public class ResultData<T> : Result
    {
        public T Data { get; set; }

        public ResultData(int statusCode, string message, T data) : base(statusCode, message)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class Result
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public Result()
        {

        }
        public Result(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
