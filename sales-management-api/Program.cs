using Core.Interfaces;
using Core.Services;
using Entities.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

// Configure to use HTTP
app.Urls.Add("https://localhost:7289");

app.Run();
