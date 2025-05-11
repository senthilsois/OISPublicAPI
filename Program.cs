var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient(); // For .NET 6+

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    //c.SwaggerEndpoint("/public_a/swagger/v1/swagger.json", "public");
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "");
    c.RoutePrefix = "swagger"; // optional, ensures it's at /swagger
});

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
