using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using OISPublic.OISDataRoom;
using OISPublic.OISDataRoomDto;
using OISPublic.Services;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using DocumentManagement.Helper;
using Microsoft.Graph.Models;
using Microsoft.AspNetCore.Authorization;


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;



namespace OISPublic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentUploadController : ControllerBase
    {
        private readonly OISDataRoomContext _context;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly PathHelper _pathHelper;
        private IWebHostEnvironment _webHostEnvironment;



        public DocumentUploadController(
        
            IWebHostEnvironment webHostEnvironment,
            OISDataRoomContext context,
            IConfiguration configuration,
            JwtTokenService jwtTokenService,
            PathHelper pathHelper 
        
          )
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
            _pathHelper = pathHelper;

         
        }

        [HttpPost("AddDocumentFromDataRoom")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddDocumentFromDataRoom()
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var form = await Request.ReadFormAsync();

                if (!int.TryParse(form["CompanyId"], out int companyId))
                    return BadRequest("Invalid CompanyId.");

                if (!Guid.TryParse(form["DataRoomId"], out Guid dataRoomId))
                    return BadRequest("Invalid DataRoomId.");

                string clientId = form["ClientId"];
                if (string.IsNullOrWhiteSpace(clientId))
                    return BadRequest("ClientId is required.");

                string permission = form["Permission"];
                if (string.IsNullOrWhiteSpace(permission))
                    return BadRequest("Permission is required.");

                if (!Guid.TryParse(form["MasterUserId"], out Guid userId))
                    return BadRequest("Invalid MasterUserId.");


                var createdByUser = await _context.DataRoomMasterUsers
     .Where(m => m.Id == userId && m.IsDeleted == false)
     .Select(m => m.Name)

     .FirstOrDefaultAsync();


                bool userExists = await _context.DataRoomUsers
                    .AnyAsync(du => du.UserId == userId && du.IsDeleted == false);

                if (!userExists)
                    return BadRequest("MasterUserId not found in DataRoomUsers.");

                var dataRoom = await _context.DataRooms
                    .Include(dr => dr.DataRoomUsers)
                    .FirstOrDefaultAsync(dr => dr.Id == dataRoomId && !dr.IsDeleted);

                if (dataRoom == null)
                    return NotFound("Data room not found.");

                var documentList = new List<DocumentDto>();
                if (form.TryGetValue("Documents", out var documentsJson) && !string.IsNullOrWhiteSpace(documentsJson))
                {
                    var jArray = JArray.Parse(documentsJson);
                    foreach (var jToken in jArray)
                    {
                        var jObj = (JObject)jToken;
                        if (!Guid.TryParse(jObj["id"]?.ToString(), out Guid docId))
                        {
                            docId = Guid.NewGuid();
                            jObj["id"] = docId.ToString();
                        }

                        var doc = jObj.ToObject<DocumentDto>();
                        if (doc != null)
                            documentList.Add(doc);
                    }
                }

                var files = form.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    byte[] fileData;
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        fileData = ms.ToArray();
                    }

                    var docFromRequest = documentList.FirstOrDefault(d => d.Name == file.FileName) ??
                                         (i < documentList.Count ? documentList[i] : new DocumentDto());

                    var metaKey = $"documentMetaDatas[{i}]";
                    var metaList = new List<DocumentMetaDataDto>();
                    if (!string.IsNullOrWhiteSpace(form[metaKey]))
                    {
                        metaList = JsonConvert.DeserializeObject<List<DocumentMetaDataDto>>(form[metaKey]);
                    }

                    var documentId = docFromRequest?.Id ?? Guid.NewGuid();
                    string fileExtension = !string.IsNullOrWhiteSpace(docFromRequest.extension)
        ? (docFromRequest.extension.StartsWith(".") ? docFromRequest.extension : "." + docFromRequest.extension)
        : Path.GetExtension(file.FileName);


                    var storagePath = await _context.FileStorageSettings
                        .Where(s => !s.IsDeleted)
                        .Select(s => s.Path)
                        .FirstOrDefaultAsync();

                    if (string.IsNullOrEmpty(storagePath))
                        return NotFound(new
                        {
                            success = false,
                            message = "Document storage path is not configured."
                        });

                    if (!Directory.Exists(storagePath))
                        Directory.CreateDirectory(storagePath);

                    var fullPath = Path.Combine(storagePath, $"{documentId}{fileExtension}");

                    if (docFromRequest.CloudSlug == "Server" && _pathHelper.AllowEncryption)
                        await EncryptFileByPart(fullPath, fileData);
                    else
                        await System.IO.File.WriteAllBytesAsync(fullPath, fileData);

                    docFromRequest.Id = documentId;
                    docFromRequest.Name = file.FileName;
                    docFromRequest.Url = $"{documentId}{fileExtension}";
                    docFromRequest.FilePath = storagePath;
                    docFromRequest.DocumentSize = file.Length;
                    docFromRequest.ClientId = clientId;
                    docFromRequest.CompanyId = companyId;
                    docFromRequest.DocumentMetaDatas = metaList;

                    if (!documentList.Any(d => d.Id == documentId))
                        documentList.Add(docFromRequest);
                }

                var existingDocNames = await _context.DataRoomFiles
                    .Where(f => f.DataRoomId == dataRoom.Id && !f.IsDeleted)
                    .Join(_context.Documents, file => file.DocumentId, doc => doc.Id, (file, doc) => doc.Name)
                    .ToListAsync();

                var duplicate = documentList.FirstOrDefault(doc =>
                    existingDocNames.Contains(doc.Name, StringComparer.OrdinalIgnoreCase));
                if (duplicate != null)
                    return Conflict(new
                    {
                        success = false,
                        message = $"Document with name '{duplicate.Name}' already exists.",
                        DuplicateName = duplicate.Name,
                        ExistingDocuments = existingDocNames
                    });

                var now = DateTime.UtcNow;

                foreach (var doc in documentList)
                {
                    var documentId = doc.Id != Guid.Empty ? doc.Id : Guid.NewGuid();

                    await _context.Database.ExecuteSqlRawAsync(
                        @"EXEC dbo.sp_AddDocumentToDataRoom 
                        @DocumentId = {0}, @Name = {1}, @Url = {2}, @Description = {3},
                        @DocumentSize = {4}, @DocType = {5}, @DocumentType = {6},
                        @CloudSlug = {7}, @FilePath = {8}, @ClientId = {9}, @CompanyId = {10},
                        @CategoryId = {11}, @DataRoomId = {12}, @UserId = {13}, 
                        @CreatedDate = {14}, @Permission = {15}, @CreatedByName = {16}",
                        documentId, doc.Name, doc.Url ?? "", doc.Description ?? "",
                        doc.DocumentSize, doc.DocType ?? "", doc.DocumentType ?? "",
                        doc.CloudSlug ?? "", doc.FilePath ?? "", doc.ClientId, doc.CompanyId,
                        dataRoom.CategoryId!.Value, dataRoom.Id, userId,
                        now, permission?.ToLower() == "inherit" ? dataRoom.DefaultPermission : permission, createdByUser
                    );

                    if (doc.DocumentMetaDatas != null)
                    {
                        foreach (var meta in doc.DocumentMetaDatas)
                        {
                            if (!string.IsNullOrWhiteSpace(meta?.Metatag))
                            {
                                await _context.Database.ExecuteSqlRawAsync(
                                    @"EXEC dbo.sp_AddDocumentMetaData 
                                    @MetaId = {0}, @DocumentId = {1}, @Metatag = {2},
                                    @CreatedBy = {3}, @CreatedDate = {4}",
                                    Guid.NewGuid(), documentId, meta.Metatag, userId, now
                                );
                            }
                        }
                    }


          
                    var auditTrail = new DataRoomAuditTrial
                    {
                        Id = Guid.NewGuid(),
                        DataRoomId = dataRoom.Id,
                       Operation = (int)DataRoomOperation.DocumentCreated,
                       CreatedDate =    DateTime.UtcNow,
                       ModifiedDate = DateTime.UtcNow,
                        ActionName = DataRoomOperation.DocumentCreated.ToString(),
                        CreatedBy = userId,
                        ModifiedBy = null,
                        DataRoomName = dataRoom.Name,
                        DocumentName = doc.Name,
                        CompanyId = dataRoom.CompanyId,
                        ClientId = dataRoom.ClientId,
                        IsDeleted = false,
                        UserId = userId

                    };

                    _context.DataRoomAuditTrials.Add(auditTrail);


                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(new
                {
                    success = true,
                    message = "Documents uploaded and saved successfully",
                    statusCode = 200
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        /// 

        [HttpGet("Encrypt")]

        private async Task EncryptFileByPart(string fullPath, byte[] fileData)
        {
            const int byteLimit = 102400;
            int currentIndex = 0;
            int partNumber = 0;
            string dir = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileNameWithoutExtension(fullPath);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            while (currentIndex < fileData.Length)
            {
                int currentPartSize = Math.Min(byteLimit, fileData.Length - currentIndex);
                byte[] filePart = new byte[currentPartSize];
                Array.Copy(fileData, currentIndex, filePart, 0, currentPartSize);
                currentIndex += currentPartSize;

                byte[] iv = GenerateIv();
                byte[] encryptedData = EncryptData(filePart, Encoding.UTF8.GetBytes(_pathHelper.AesEncryptionKey), iv);
                string partFileName = Path.Combine(dir, $"{fileName}.part{partNumber}");

                using (var fs = new FileStream(partFileName, FileMode.Create, FileAccess.Write))
                {
                    await fs.WriteAsync(iv);
                    await fs.WriteAsync(encryptedData);
                }

                partNumber++;
            }
        }

        public static byte[] GenerateIv()
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }

        private byte[] EncryptData(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }


        [HttpGet("Download/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadDocument(Guid id, [FromQuery] bool isVersion)
        {
            try
            {
                // 1. Fetch document or version
                var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
                //var version = await _context.DocumentVersions.FirstOrDefaultAsync(v => v.Id == id);

                if (document == null)
                    return NotFound("Document not found.");

                string urlPath =  document.Url;
                string cloudSlug =   document.CloudSlug;

                if (string.IsNullOrEmpty(urlPath))
                    return NotFound("File path not found.");

                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(urlPath);
                string fileExtension = Path.GetExtension(urlPath);

                byte[] fileBytes;

                if (cloudSlug == "Google")
                {
                    var googleDetails = await _context.CloudDetails
                        .FirstOrDefaultAsync(x => x.AuthType == "Googledrive" && x.IsDeleted  == false&& x.IsDefault == true
                                                 );

                    if (googleDetails == null)
                        return NotFound("Google Drive credentials not configured.");

                    fileBytes = await GoogleDrive.DownloadFilePartsFromGoogleDriveByName(
                        fileNameWithoutExt,
                        _pathHelper.AesEncryptionKey,
                        googleDetails.AuthUrl,
                        googleDetails.AuthName,
                        googleDetails.AuthToken);

                    if (fileBytes == null || fileBytes.Length == 0)
                        return NotFound("Unable to fetch or decrypt file from Google Drive.");

                    return File(fileBytes, GetContentType(fileExtension), $"{fileNameWithoutExt}{fileExtension}");
                }
                else if (urlPath.Contains("amazonaws.com"))
                {
                    using var httpClient = new HttpClient();
                    var fileStream = await httpClient.GetStreamAsync(urlPath);
                    return File(fileStream, GetContentType(urlPath), Path.GetFileName(urlPath));
                }
                else
                {
                    // Local file parts
                    var absolutePath = Path.Combine(document.FilePath);
                    var fileBaseNames = Path.Combine(Path.GetDirectoryName(absolutePath), Path.GetFileNameWithoutExtension(absolutePath));
                    var fileBaseName = fileBaseNames+'\\'+ fileNameWithoutExt;
                    var fileParts = new List<byte[]>();
                    int partIndex = 0;
                    string partFilePath;

                    while (System.IO.File.Exists(partFilePath = $"{fileBaseName}.part{partIndex}"))
                    {
                        byte[] partData = await System.IO.File.ReadAllBytesAsync(partFilePath);
                        fileParts.Add(partData);
                        partIndex++;
                    }

                    if (fileParts.Count == 0)
                        return NotFound("File not found.");

                    // Decrypt if enabled
                    if (_pathHelper.AllowEncryption)
                    {
                        string keyString = _pathHelper.AesEncryptionKey;
                        byte[] key = Encoding.UTF8.GetBytes(keyString);

                        if (key.Length != 16 && key.Length != 24 && key.Length != 32)
                            return StatusCode(500, "Invalid encryption key length.");

                        var decryptedParts = fileParts.Select(part =>
                        {
                            byte[] iv = part.Take(16).ToArray();
                            byte[] encryptedData = part.Skip(16).ToArray();
                            return DecryptData(encryptedData, key, iv);
                        }).ToList();

                        fileBytes = CombineByteArrays(decryptedParts);
                    }
                    else
                    {
                        fileBytes = CombineByteArrays(fileParts);
                    }

                    return File(fileBytes, GetContentType(absolutePath), Path.GetFileName(absolutePath));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string GetContentType(string path)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        private byte[] CombineByteArrays(List<byte[]> arrays)
        {
            using var ms = new MemoryStream();
            foreach (var array in arrays)
            {
                ms.Write(array, 0, array.Length);
            }
            return ms.ToArray();
        }

        private byte[] DecryptData(byte[] encryptedData, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(encryptedData);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var result = new MemoryStream();
            cs.CopyTo(result);
            return result.ToArray();
        }



        ///documnet delete from dataroom
        ///

        [HttpDelete("DeleteFromDataRoom")]
        public async Task<IActionResult> DeleteDocumentFromDataRoom([FromQuery] Guid documentId, 
            [FromQuery] Guid dataRoomId, [FromQuery] Guid deletedBy)
        {
            if (documentId == Guid.Empty || dataRoomId == Guid.Empty || deletedBy == Guid.Empty)
                return BadRequest("Invalid parameters.");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
              
                var dataRoomFile = await _context.DataRoomFiles
                    .FirstOrDefaultAsync(x =>
                        x.DocumentId == documentId &&
                        x.DataRoomId == dataRoomId &&
                        !x.IsDeleted);

                if (dataRoomFile == null)
                    return NotFound("Document not found in the specified data room.");

                dataRoomFile.IsDeleted = true;
                dataRoomFile.DeletedBy = deletedBy;
                dataRoomFile.DeletedDate = DateTime.UtcNow;
                _context.DataRoomFiles.Update(dataRoomFile);

            
                var document = await _context.Documents
                    .FirstOrDefaultAsync(d => d.Id == documentId && !d.IsDeleted);

                string documentName = string.Empty;
                if (document != null)
                {
                    document.IsDeleted = true;
                    document.DeletedBy = deletedBy;
                    document.DeletedDate = DateTime.UtcNow;
                    documentName = document.Name;
                    _context.Documents.Update(document);
                }

              
                var dataRoom = await _context.DataRooms
                    .AsNoTracking()
                    .FirstOrDefaultAsync(dr => dr.Id == dataRoomId);

          
                await _context.SaveChangesAsync();

       
                var auditTrail = new DataRoomAuditTrial
                {
                    Id = Guid.NewGuid(),
                    DataRoomId = dataRoomId,
                    Operation = (int)DataRoomOperation.DocumentDeleted,
                    ActionName = DataRoomOperation.DocumentDeleted.ToString(),
                    CreatedBy = deletedBy,
                    ModifiedBy = null,
                    DataRoomName = dataRoom?.Name,
                    DocumentName = documentName,
                    CompanyId = dataRoom?.CompanyId,
                    ClientId = dataRoom?.ClientId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                };

                await _context.DataRoomAuditTrials.AddAsync(auditTrail);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(new
                {
                    success = true,
                    message = "Document deleted from data room successfully",
                    statusCode = 200
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }






    }
}
