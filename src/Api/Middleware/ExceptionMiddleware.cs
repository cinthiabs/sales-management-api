using Domain.Dtos;
using System.Net;
using System.Text.Json;

namespace Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ApiResponseDto> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ApiResponseDto> logger, IHostEnvironment environment)
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
                    new ApiResponseDto(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace.ToString()) :
                    new ApiResponseDto(context.Response.StatusCode.ToString(), ex.Message, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
