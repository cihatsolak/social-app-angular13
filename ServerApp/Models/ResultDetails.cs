namespace ServerApp.Models
{
    public class ResultDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ResultDetails()
        {

        }

        public ResultDetails(int statusCode, string message)
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
