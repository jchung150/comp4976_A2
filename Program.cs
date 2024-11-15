using ExoticPlantsAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// // Configure DbContext to load the connection string only in Development environment
// string connectionString;
// if (builder.Environment.IsDevelopment())
// {
//     // Retrieve the connection string from appsettings.Development.json
//     connectionString = builder.Configuration.GetConnectionString("ExoticPlantsDatabase");
// }
// else
// {
   
//     connectionString = null; 
// }

// if (!string.IsNullOrEmpty(connectionString))
// {
//     builder.Services.AddDbContext<ExoticPlantsContext>(options =>
//         options.UseSqlite(connectionString));
// }

// Retrieve the connection string from configuration
string connectionString = builder.Configuration.GetConnectionString("ExoticPlantsDatabase");

if (!string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<ExoticPlantsContext>(options =>
        options.UseSqlite(connectionString));
}
else
{
    throw new InvalidOperationException("Database connection string is not configured.");
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations and create the database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ExoticPlantsContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
