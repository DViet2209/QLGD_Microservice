using CategoryWebAPI;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var dbHost = "DUCVIET";
var dbName = "dms_category ";
var dbPassword = "220901";

var connectionString = $"Data Source ={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}; TrustServerCertificate=True";

builder.Services.AddDbContext<CategoryDbContext>(opt => opt.UseSqlServer(connectionString));


// Configure the HTTP request pipeline.
var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
