using Microsoft.EntityFrameworkCore;
using ContactWebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var dbHost = "DUCVIET";
var dbName = "dms_contact ";
var dbPassword = "220901";

var connectionString = $"Data Source ={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}; TrustServerCertificate=True";

builder.Services.AddDbContext<ContactDbContext>(opt => opt.UseSqlServer(connectionString));

// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
