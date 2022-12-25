namespace DDD.API.Exceptions
{
    public class APIException : Exception
    {

        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;

        public APIException(string message, int status) : base(message) {
            StatusCode = status;
        }
       
    }
}
