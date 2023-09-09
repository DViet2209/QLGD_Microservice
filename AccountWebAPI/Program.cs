using AccountWebAPI;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var dbHost = "DUCVIET";
var dbName = "dms_account2";
var dbPassword = "220901";

var connectionString = $"Data Source ={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}; TrustServerCertificate=True";

builder.Services.AddDbContext<AccountDbContext>(opt => opt.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
