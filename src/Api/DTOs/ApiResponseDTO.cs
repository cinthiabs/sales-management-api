namespace Api.DTOs
{
    public class ApiResponseDTO
    {
        public ApiResponseDTO(string statusCode, string message, string details) 
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
