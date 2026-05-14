using Microsoft.EntityFrameworkCore;
using ProductSimple.Domain;
using ProductSimple.Repositories;
using ProductSimple.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration
    .GetValue<string>("APPLICATIONINSIGHTS_CONNECTION_STRING")
    ?? builder.Configuration
        .GetValue<string>("ApplicationInsights:ConnectionString");


// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("MyApiDb"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

if (!string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddApplicationInsightsTelemetry(options =>
        options.ConnectionString = connectionString);
}; // sera configuré via env var

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
