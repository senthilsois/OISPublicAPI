﻿using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Http;
using System.Reflection.Metadata;
namespace OISPublic.Controllers
{
    public class publicAPIHandler : Controller
    {
        private readonly IConfiguration _configuration;
        public IWebHostEnvironment _webHostEnvironment;
        private readonly HttpClient _httpClient;
        public publicAPIHandler(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _httpClient = httpClientFactory.CreateClient();
        }


        [HttpGet("getDocuments")]
        public async Task<byte[]> GetDataFromExternalApi(string documentId, string filename)
        {
            var url = _configuration["DomainName"] + "api/Document/" + documentId + "/download";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error calling external API");
            }

            return await response.Content.ReadAsByteArrayAsync(); // Return byte array directly
        }

        [HttpGet]
            [Route("public_link/verify/{shortCode}/{password}")]
            public async Task<IActionResult> verifyProtectPassword(string password,string shortCode)
        {
            string connectionString = _configuration.GetConnectionString("dmsdb_connectionstring");
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT PasswordHash FROM DocumentExternalLinkSharing WHERE shortCode=@shortCode", connection))
                    {
                        command.Parameters.AddWithValue("@shortCode", shortCode); // Prevent SQL injection
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Move to the first row
                            {
                                var passwordHash = reader["PasswordHash"]?.ToString();
                                if (password.Equals(passwordHash))
                                {
                                    return Ok(new
                                    {
                                        status = true,
                                        message = "Password verified successfully"
                                    });
                                }
                                else
                                {
                                    return Ok(new
                                    {
                                        status = false,
                                        message = "Invalid Password"
                                    });
                                }
                            }
                            else
                            {
                                return Ok(new
                                {
                                    status = false,
                                    message = "Short code not found"
                                });
                            }
                        }
                    }

                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, new { error = "Database error", details = sqlEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred", details = ex.Message });
            }

        }

        //Get API to create the external link
        [HttpGet]
        [Route("/public_link/{shortCode}")]
        public async Task<IActionResult> GeneratePublicLinkHandler(string shortCode)
        {
            string connectionString = _configuration.GetConnectionString("dmsdb_connectionstring");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertDocumentAuditData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ShortCode", string.IsNullOrEmpty(shortCode) ? (object)DBNull.Value : shortCode);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader.HasColumn("Message"))
                                {
                                    // Handle failure case
                                    var errorMessage = reader["Message"]?.ToString() ?? "Unknown error.";
                                    return BadRequest(new { error = errorMessage });
                                }
                                else if (reader.HasColumn("DocumentId") && reader.HasColumn("DocumentName"))
                                {
                                    // Handle success case
                                    var documentId = reader["DocumentId"].ToString();
                                    var fileName = reader["Url"].ToString();
                                    var isProtected = reader["Protected"];
                                    var isAllowDownload = reader["AllowDownload"];
                                    var documentBytes = await GetDataFromExternalApi(documentId, fileName); // Now returns byte[]

                                    return Ok(new
                                    {
                                        DocumentBytes = Convert.ToBase64String(documentBytes),
                                        IsProtected = isProtected,
                                        IsAllowDownload = isAllowDownload
                                    });
                                }
                            }
                        }

                        return StatusCode(500, new { error = "Unexpected response from stored procedure." });
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, new { error = "Database error", details = sqlEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred", details = ex.Message });
            }
        }




        // public async Task<IActionResult> GeneratePublicLinkHandler(string shortCode)
        // {
        //     using var connection = new SqlConnection(_configuration.GetConnectionString("dmsdb_connectionstring"));
        //     await connection.OpenAsync();
        //     if (connection.State == ConnectionState.Open)
        //     {

        //         if (!string.IsNullOrEmpty(shortCode))
        //         {

        //             using var getDMSConnection = new SqlConnection(_configuration.GetConnectionString("dmsdb_connectionstring"));
        //             await getDMSConnection.OpenAsync();
        //             if (getDMSConnection.State == ConnectionState.Open)
        //             {




        //                 var dataSet = await getDMSConnection.QueryFirstOrDefaultAsync("SELECT * from DocumentExternalLinkSharing WHERE shortCode= @shortCode", new { shortCode = shortCode });
        //                 if (dataSet != null)
        //                 {
        //                     if (dataSet.IsTimeBound &&
        //(dataSet.EndDate.HasValue && dataSet.EndDate.Value < DateTime.Today ||
        //(dataSet.EndDate.HasValue && dataSet.EndDate.Value == DateTime.Today &&
        // dataSet.EndTime.HasValue && dataSet.EndTime.Value < DateTime.UtcNow.TimeOfDay)))
        //                     {
        //                         return BadRequest("Your document has expired!");
        //                     }

        //                     var documentId = dataSet.DocumentId;
        //                     var affectedRows = await connection.ExecuteAsync(
        //                         "INSERT  DocumentAuditTrails SET OperationName = @OperationName WHERE DocumentId = @DocumentId",
        //                         new { OperationName = 1, DocumentId = dataSet.DocumentId }
        //                     );

        //                     if (affectedRows > 0)
        //                     {
        //                         //Get the document path from the public folder and return the document url

        //                         using var DMSConnection = new SqlConnection(_configuration.GetConnectionString("dmsdb_connectionstring"));
        //                         await DMSConnection.OpenAsync();
        //                         if (DMSConnection.State == ConnectionState.Open)
        //                         {
        //                             var document = await DMSConnection.QueryFirstOrDefaultAsync("SELECT * from Documents WHERE id = @DocumentId", new { DocumentId = documentId });
        //                             if (document != null)
        //                             {
        //                                 var extension = Path.GetExtension(document.Url);
        //                                 var fileName = documentId + "." + extension;

        //                                 var documentBytes = await GetDataFromExternalApi(documentId.ToString(), fileName.ToString());
        //                                 if (documentBytes == null)
        //                                 {

        //                                     return BadRequest("Documet cannot be loaded. Please try again!");

        //                                 }


        //                                 //var result = new { name = fileName, blob = documentBytes};
        //                                 //return Ok(result);
        //                                 return documentBytes;




        //                                 //// Retrieve the directory path from appsettings.json  
        //                                 //var dirPath = _configuration["publicDir"];
        //                                 //var link = Path.Combine(dirPath, fileName);
        //                                 //// Get the absolute path of the directory  
        //                                 //var absolutePath = Path.GetFullPath(link);

        //                                 //// Get the relative path of the directory  
        //                                 //var relativePath = Path.GetRelativePath(_webHostEnvironment.ContentRootPath, absolutePath);
        //                                 //var domainName = _configuration["DomainName"];
        //                                 //var fullPathWithDomain = $"{domainName}/{relativePath.Replace("\\", "/")}";


        //                                 ////var _named = $"<a href=\"{link}\" target=\"_blank\">{fileName}</a>";
        //                                 //return Ok(new { sharedLink = fullPathWithDomain });
        //                             }
        //                             else
        //                             {
        //                                 return BadRequest("Unable to create the link!");
        //                             }
        //                         }
        //                         else
        //                         {
        //                             return BadRequest("Something went wrong!");
        //                         }
        //                     }
        //                     else
        //                     {
        //                         return BadRequest("Unable to connect the server!");
        //                     }
        //                 }
        //                 else
        //                 {
        //                     return BadRequest("Document id is not valid!");
        //                 }
        //             }
        //             else
        //             {
        //                 return BadRequest("Unable to connect the server!");
        //             }
        //         }
        //         else
        //         {
        //             return BadRequest("Url is not valid!");
        //         }
        //     }
        //     else
        //     {
        //         return BadRequest("Unable to connect the server!");
        //     }
        // }
    }

    // Extension method to check if a column exists in SqlDataReader
    public static class SqlDataReaderExtensions
    {
        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
