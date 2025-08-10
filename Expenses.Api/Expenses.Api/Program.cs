var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
