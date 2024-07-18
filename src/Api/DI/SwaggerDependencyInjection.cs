using Microsoft.OpenApi.Models;

namespace Api.DI
{
    public static partial class SwaggerDependencyInjection
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Sales Management",
                    Version = "v1",
                    Description = "API for sales management."
                });
            });
        }

        public static void AddCorsProject(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }
    }
    
}
