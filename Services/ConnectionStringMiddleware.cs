using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using OISPublic;

public class ConnectionStringMiddleware
{
    private readonly RequestDelegate _next;

    public ConnectionStringMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ConnectionStringProvider provider)
    {

        provider.ConnectionStringName = "dmsdb_connectionstring";

        if (context.Request.Headers.TryGetValue("X-Db-Name", out var dbName) &&
            dbName.ToString().ToLower() == "public")
        {
            provider.ConnectionStringName = "oispublicdb_connectionstring";
        }

        await _next(context);
    }
}
