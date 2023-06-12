using Microsoft.EntityFrameworkCore;
using ProductWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var dbHost =  Environment.GetEnvironmentVariable("DB_HOST");   //"localhost";            
var dbName =   Environment.GetEnvironmentVariable("DB_NAME");    //"dms_product";         
var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");  //"Password1@123";     
var connectionString = $"Server={dbHost};port=3306;Database={dbName};user=root;Password={dbPassword}";//Trusted_Connection=True;Encrypt=False;This is for the Db.I took it out

builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseMySQL(connectionString));//This is bcos of the mssql container we are using
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

app.UseAuthorization();

app.MapControllers();

app.Run();
