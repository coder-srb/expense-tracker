using Expenses.Api.Data;
using Expenses.Api.Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configured Cors
builder.Services.AddCors(opt => opt.AddPolicy("AllowAll",
    opt => opt.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin())
);


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

app.UseAuthorization();

app.MapControllers();

app.Run();
