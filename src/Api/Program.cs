using Api.DI;
using Api.Middleware;
using Datadog.Trace;
using Datadog.Trace.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPresentationDI();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCorsProject();
builder.Services.AddAutoMapper(typeof(Program));


var tracerSettings = TracerSettings.FromDefaultSources();
tracerSettings.ServiceName = "sales-management-api";
Tracer.Configure(tracerSettings);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();


app.Run();
