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
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Load JWT settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

// --- Service Configuration ---
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

// JWT Authentication
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddAuthorization();
builder.Services.AddSingleton<PathHelper>();
builder.Services.AddSignalR();
builder.Services.AddHostedService<NotificationWatcherService>();
builder.Services.AddScoped<NotificationService>();

var app = builder.Build();

// --- Global Error Handling ---
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionFeature?.Error;

        var errorDetails = new
        {
            Message = "A server error occurred.",
            ExceptionMessage = exception?.Message,
            ExceptionType = exception?.GetType().Name,
            StackTrace = exception?.StackTrace,
            InnerException = exception?.InnerException?.Message,
            Path = exceptionFeature?.Path
        };

        await context.Response.WriteAsJsonAsync(errorDetails);
    });
});


app.UseStaticFiles();
app.UseSwagger(); 
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
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
