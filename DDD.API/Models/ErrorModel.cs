namespace DDD.API.Models
{
    public class ErrorModel
    {
        public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;
        public string? Message { get; set; } = string.Empty;
    }
}
