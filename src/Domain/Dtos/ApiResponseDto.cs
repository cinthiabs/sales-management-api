namespace Domain.Dtos
{
    public class ApiResponseDto
    {
        public ApiResponseDto(string statusCode, string message, string details) 
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
