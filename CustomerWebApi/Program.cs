using CustomerWebApi;
using JwtAuthenticationManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenHandler.JWT_SECURITY_KEY))
    };
});

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");              //"DESKTOP-0FRMSIG";  
var dbName = Environment.GetEnvironmentVariable("DB_NAME");             //"FullStackDb";
var dbPassword =Environment.GetEnvironmentVariable("DB_MSSQL_SA_PASSWORD");       //"password1";
var connectionString = $"Server={dbHost};Database={dbName};User ID=sa;Password={dbPassword}";//Trusted_Connection=True;Encrypt=False;This is for the Db.I took it out

builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(connectionString));//This is bcos of the mssql container we are using
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

app.UseAuthentication();    
app.UseAuthorization();

app.MapControllers();

app.Run();
