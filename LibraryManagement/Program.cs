using LibraryManagement.Context;
using Microsoft.EntityFrameworkCore;
using RedisEntegration.Interfaces;
using RedisEntegration.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
var redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);

// Hizmetleri ekleme
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("SqlServer")!;
    options.UseSqlServer(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
