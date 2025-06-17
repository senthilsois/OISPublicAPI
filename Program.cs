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
app.UseSwaggerUI();
//app.UseSwaggerUI(c =>
//{
//    //c.SwaggerEndpoint("/public_a/swagger/v1/swagger.json", "public");
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "");
//    c.RoutePrefix = "swagger"; // optional, ensures it's at /swagger
//});

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();


// Fix for CS0234: Ensure the required NuGet package is installed and the namespace is correctly referenced.
// The 'Microsoft.Identity.Web.UI' namespace is part of the 'Microsoft.Identity.Web' package, but it requires the 'Microsoft.Identity.Web.UI' assembly.
// Install the package via NuGet if not already installed:
//// Command: dotnet add package Microsoft.Identity.Web.UI
//using Microsoft.AspNetCore.Authentication.OpenIdConnect;
//using Microsoft.Identity.Web;
//using Microsoft.Identity.Web.UI;

//var builder = WebApplication.CreateBuilder(args);

//// Configure authentication with Azure AD
//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
//    .EnableTokenAcquisitionToCallDownstreamApi()
//    .AddMicrosoftGraph(builder.Configuration.GetSection("Graph"))
//    .AddInMemoryTokenCaches();

//// Authorization setup
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = options.DefaultPolicy;
//});

//// Register Graph API + other services
//builder.Services.AddHttpClient();
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Middleware
//app.UseSwagger();
//app.UseSwaggerUI();

//app.UseHttpsRedirection();
//app.UseAuthentication();  // IMPORTANT: This must come before UseAuthorization
//app.UseAuthorization();

//app.MapControllers();
//app.Run();
