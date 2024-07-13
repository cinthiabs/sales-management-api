using Api.Middleware;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Sales Management",
        Version = "v1",
        Description = "API for sales management."
    });
});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
});
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ISale, SaleService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<ICost, CostService>();
builder.Services.AddScoped<IUpload, UploadService>();
builder.Services.AddScoped<ICostRepository, CostRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
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
