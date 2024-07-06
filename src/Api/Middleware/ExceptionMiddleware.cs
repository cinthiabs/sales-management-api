using Api.DTOs;
using System.Net;
using System.Text.Json;

namespace Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ApiResponseDTO> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ApiResponseDTO> logger, IHostEnvironment environment)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
            _environment = environment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType ="application/json";
                context.Response.StatusCode =(int)HttpStatusCode.InternalServerError;

                var response = _environment.IsDevelopment() ?
                    new ApiResponseDTO(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace.ToString()) :
                    new ApiResponseDTO(context.Response.StatusCode.ToString(), ex.Message, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
