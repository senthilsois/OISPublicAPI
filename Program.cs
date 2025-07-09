using Microsoft.EntityFrameworkCore;
using OISPublic.OISDataRoom;
using OISPublic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OISPublic.Services;
using DocumentManagement.Helper;
using Microsoft.Graph.Models.ExternalConnectors;

var builder = WebApplication.CreateBuilder(args);


var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];







builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});






builder.Services.AddScoped<ConnectionStringProvider>();

builder.Services.AddDbContext<OISDataRoomContext>((serviceProvider, options) =>
{
    var provider = serviceProvider.GetRequiredService<ConnectionStringProvider>();
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var connectionStringName = provider.ConnectionStringName;
    var connectionString = configuration.GetConnectionString(connectionStringName);

    if (string.IsNullOrEmpty(connectionString))
        throw new InvalidOperationException("The ConnectionString property has not been initialized.");

    options.UseSqlServer(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient(); // For .NET 6+

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddAuthorization();
builder.Services.AddSingleton<PathHelper>();
var app = builder.Build();

// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI();



app.UseCors("AllowAll");

app.UseMiddleware<ConnectionStringMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
builder.Services.AddDbContext<OISDataRoomContext>((serviceProvider, options) =>
{
    var provider = serviceProvider.GetRequiredService<ConnectionStringProvider>();
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var connectionStringName = provider.ConnectionStringName;
    var connectionString = configuration.GetConnectionString(connectionStringName);

    if (string.IsNullOrEmpty(connectionString))
        throw new InvalidOperationException("The ConnectionString property has not been initialized.");

    options.UseSqlServer(connectionString);
});


