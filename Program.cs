using Microsoft.EntityFrameworkCore;
using OISPublic.OISDataRoom;
using OISPublic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OISPublic.Services;
using DocumentManagement.Helper;
using Microsoft.Graph.Models.ExternalConnectors;
using OISPublic.Helper;

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
builder.Services.AddSignalR();
builder.Services.AddHostedService<NotificationWatcherService>();
builder.Services.AddScoped<NotificationService>();


var app = builder.Build();

// Enable Swagger in all environments
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.Use(async (context, next) =>
{
  if(context.Request.Path == "/")
    {

        context.Response.Redirect("/swagger/index.html");
        return;

    }
    await next();
});


app.UseCors("AllowAll");

app.UseMiddleware<ConnectionStringMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/hubs/notifications");

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


