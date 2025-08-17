using Expenses.Api.Data;
using Expenses.Api.Data.Services;
using Expenses.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configured Cors
builder.Services.AddCors(opt => opt.AddPolicy("AllowAll",
    opt => opt.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin())
);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "srb.net",     //builder.Configuration["Jwt:Issuer"],
            ValidAudience = "srb.net",    //builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-very-secure-secret-key-32-chars-long"))   // builder.Configuration["Jwt:Key"]
        };
    });

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); // For password hashing

// Configured EntityFrameworkCore
var connectionString = builder.Configuration.GetConnectionString("myConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString)); // Configure Entity Framework

// AddScoped | AddSingleton | AddTransient
builder.Services.AddScoped<ITransactionsService, TransactionsService>(); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   // generates swagger for API documentation

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // serves the generated Swagger as a JSON endpoint
    app.UseSwaggerUI(); // serves the Swagger UI for interactive API documentation
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
