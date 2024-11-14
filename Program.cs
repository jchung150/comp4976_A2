using ExoticPlantsAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext to load the connection string only in Development environment
string connectionString;
if (builder.Environment.IsDevelopment())
{
    // Retrieve the connection string from appsettings.Development.json
    connectionString = builder.Configuration.GetConnectionString("ExoticPlantsDatabase");
}
else
{
   
    connectionString = null; 
}

if (!string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<ExoticPlantsContext>(options =>
        options.UseSqlite(connectionString));
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
