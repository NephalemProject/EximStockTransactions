using EximStockTransactions.Application;
using EximStockTransactions.Infrastructure;
using EximStockTransactions.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddApplicationServices();  // Register application services
builder.Services.AddInfrastructureServices(builder.Configuration); // Register repositories

// Define CORS policy to allow requests from specified origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("eximStockTransactions", policy =>
        policy
            .WithOrigins("https://localhost:3000", "https://localhost:5000", "https://localhost:5001", "http://localhost:5000", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Register SQLite DB
builder.Services.AddDbContext<EximStockTransactionsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLite")));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EximStockTransactions API v1");
        c.RoutePrefix = string.Empty; // serve Swagger at root
    });
}

app.UseHttpsRedirection();
app.UseCors("eximStockTransactions");
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();