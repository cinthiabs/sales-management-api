using Application.Interfaces;
using Core.Repositories;
using Core.Services;
using Data.Infrastructure.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");


app.UseAuthorization();

app.MapControllers();


app.Run();
