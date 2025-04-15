using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using System.Data;
using System.Data.SqlClient;
namespace OISPublic.Controllers
{
    public class publicAPIHandler : Controller
    {
        private readonly IConfiguration _configuration;

        public publicAPIHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get API to create the external link
        [HttpGet]
        [Route("/public_link/{documentId}/{shareId}")]
        public async Task<IActionResult> GeneratePublicLinkHandler(string documentId, string shareId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("oispublicdb_connectionstring"));
            await connection.OpenAsync();

            if (!string.IsNullOrEmpty(documentId))
            {
                var affectedRows = await connection.ExecuteAsync(
                    "UPDATE DocumentAuditTrails SET OperationName = @OperationName WHERE DocumentId = @DocumentId",
                    new { OperationName = 1, DocumentId = documentId }
                );
                return Ok(affectedRows);
            }
            else
            {
                return BadRequest("Document id is not valid!");
            }
        }
    }
}
