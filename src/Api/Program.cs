using Api.DI;
using Api.Middleware;
using Application.AutoMapper;
using Application.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
); 
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPresentationDI();
builder.Services.AddSwaggerDocumentation(builder.Configuration);
builder.Services.AddCorsProject();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
