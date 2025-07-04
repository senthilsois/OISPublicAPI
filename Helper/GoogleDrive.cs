using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Google.Apis.Download;
using Microsoft.Identity.Client;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;






namespace DocumentManagement.Helper
{

    public static class GoogleDrive
    {
        private static readonly string[] Scopes = { DriveService.Scope.Drive }; // Full Drive permissions
        private static readonly string ApplicationName = "NexaOne"; // Replace with your app name
        private static readonly string ServiceAccountKeyPath = "credentials.json"; // Path to your service account key file

        //private static readonly string ServiceAccountKeyPath = "https://localhost:44313/googledrive/b30b40fd-160b-442d-9744-9884be96b4bb.json";

        public static async Task<Dictionary<string, object>> UploadToGoogleDriveOLD(Stream fileStream, string fileName)
        {
            string ServiceAccountKeyPaths = ServiceAccountKeyPath;
            var result = new Dictionary<string, object> { { "status", 0 } };

            try
            {
                // Authenticate using the service account
                GoogleCredential credential;

                 
                using (var stream = new FileStream(ServiceAccountKeyPaths, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
                }

                // Create Drive API service
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Create metadata for the file
                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = fileName, // Use the provided file name
                    Parents = new List<string> { "1cC6tDRgLvlvROO-RN8YUZX28w_vKRsLJ" } // Replace with your Google Drive folder ID
                };

                // Upload the file (stream-based upload)
                var request = service.Files.Create(fileMetadata, fileStream, "application/octet-stream");
                request.Fields = "id, webViewLink"; // Retrieve the file ID and web view link

                var uploadResponse = await request.UploadAsync();

                if (uploadResponse.Status == UploadStatus.Completed)
                {
                    result["status"] = 1;
                    result["fileId"] = request.ResponseBody.Id; // File ID
                    result["webViewLink"] = request.ResponseBody.WebViewLink; // URL to view the uploaded file
                }
                else
                {
                    result["message"] = $"File upload failed. Status: {uploadResponse.Status}";
                    if (uploadResponse.Exception != null)
                    {
                        result["errorDetails"] = uploadResponse.Exception.Message;
                        Console.WriteLine($"Error Details: {uploadResponse.Exception}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                result["message"] = $"Error: {ex.Message}";
            }

            return result;
        }



        public static async Task<byte[]> DownloadFilePartsFromGoogleDriveByNameOLD(string fileNameBase, string encryptionKey)
        {
            try
            {
                // Authenticate using the service account
                GoogleCredential credential;
                using (var stream = new FileStream(ServiceAccountKeyPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
                }

                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                int partIndex = 0;
                var decryptedParts = new List<byte[]>();

                // Download all parts
                while (true)
                {
                    try
                    {
                        // Construct part file name dynamically
                        string partFileName = $"{fileNameBase}.part{partIndex}";

                        // Search for the file using its name
                        var listRequest = service.Files.List();
                        listRequest.Q = $"name = '{partFileName}' and trashed = false";
                        listRequest.Fields = "files(id, name)";
                        var fileList = await listRequest.ExecuteAsync();

                        if (fileList.Files == null || fileList.Files.Count == 0)
                        {
                            // Break the loop if no more file parts are found
                            break;
                        }

                        // Get the file ID of the part
                        string partFileId = fileList.Files.First().Id;

                        // Request the file part from Google Drive
                        var request = service.Files.Get(partFileId);
                        using (var memoryStream = new MemoryStream())
                        {
                            await request.DownloadAsync(memoryStream);
                            byte[] encryptedPart = memoryStream.ToArray();

                            // Extract the key and IV
                            byte[] key = Encoding.UTF8.GetBytes(encryptionKey);
                            byte[] iv = encryptedPart.Take(16).ToArray();
                            byte[] encryptedData = encryptedPart.Skip(16).ToArray();

                            // Decrypt the file part
                            byte[] decryptedPart = DecryptData(encryptedData, key, iv);
                            decryptedParts.Add(decryptedPart);
                        }

                        partIndex++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error downloading file part '{fileNameBase}.part{partIndex}': {ex.Message}");
                        throw;
                    }
                }

                // Combine all parts
                byte[] fileBytes = CombineByteArrays(decryptedParts);
                return fileBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading or decrypting file parts: {ex.Message}");
                throw;
            }
        }

        private static byte[] CombineByteArrays(IEnumerable<byte[]> arrays)
        {
            int totalLength = arrays.Sum(arr => arr.Length);
            byte[] result = new byte[totalLength];

            int offset = 0;
            foreach (var array in arrays)
            {
                Buffer.BlockCopy(array, 0, result, offset, array.Length);
                offset += array.Length;
            }

            return result;
        }

        private static string GetContentTypes(string path)
        {
            var types = new Dictionary<string, string>
    {
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".png", "image/png"},
        {".gif", "image/gif"},
        {".doc", "application/msword"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".ppt", "application/vnd.ms-powerpoint"},
        {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
        {".pdf", "application/pdf"},
        {".mp3", "audio/mpeg"},
        {".wav", "audio/wav"},
        {".ogg", "audio/ogg"},
        {".m4a", "audio/mp4"},
        {".aac", "audio/aac"},
        {".flac", "audio/flac"},
        {".mp4", "video/mp4"},
        {".avi", "video/x-msvideo"},
        {".mov", "video/quicktime"},
        {".wmv", "video/x-ms-wmv"},
        {".flv", "video/x-flv"},
        {".mkv", "video/x-matroska"},
        {".webm", "video/webm"},
        {".txt", "text/plain"}
    };

            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

        private static byte[] DecryptData(byte[] encryptedData, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedData, 0, encryptedData.Length);
                        cs.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
        }





        public static async Task<Dictionary<string, object>> UploadToGoogleDriveWorking(Stream fileStream, string fileName, string AuthUrl, string Authname, string FolderId)
        {
            string serviceAccountKeyPath = Path.GetTempFileName(); // Temporary file path
            var result = new Dictionary<string, object> { { "status", 0 } };

            try
            {
                //Download the file from the AuthUrl and save it locally
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(AuthUrl);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Failed to download the file. Status code: {response.StatusCode}");
                    }

                    var fileBytes = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(serviceAccountKeyPath, fileBytes);
                }

                // Authenticate using the downloaded service account key file
                GoogleCredential credential;
                using (var stream = new FileStream(ServiceAccountKeyPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
                }

                // Create Drive API service
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = Authname,
                });

                // Create metadata for the file
                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = fileName,
                    Parents = new List<string> { FolderId } // Replace with your Google Drive folder ID
                };

                // Upload the file (stream-based upload)
                var request = service.Files.Create(fileMetadata, fileStream, "application/octet-stream");
                request.Fields = "id, webViewLink";

                var uploadResponse = await request.UploadAsync();

                if (uploadResponse.Status == UploadStatus.Completed)
                {
                    result["status"] = 1;
                    result["fileId"] = request.ResponseBody.Id;
                    result["webViewLink"] = request.ResponseBody.WebViewLink;
                }
                else
                {
                    result["message"] = $"File upload failed. Status: {uploadResponse.Status}";
                    if (uploadResponse.Exception != null)
                    {
                        result["errorDetails"] = uploadResponse.Exception.Message;
                        Console.WriteLine($"Error Details: {uploadResponse.Exception}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                result["message"] = $"Error: {ex.Message}";
            }
            finally
            {
                // Clean up the temporary file
                if (File.Exists(serviceAccountKeyPath))
                {
                    File.Delete(serviceAccountKeyPath);
                }
            }

            return result;
        }




        public static async Task<Dictionary<string, object>> UploadToGoogleDrive(Stream fileStream, string fileName, string AuthUrl, string Authname, string FolderId)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(AuthUrl))
                throw new ArgumentNullException(nameof(AuthUrl), "AuthUrl cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(FolderId))
                throw new ArgumentNullException(nameof(FolderId), "FolderId cannot be null or empty.");
            if (fileStream == null)
                throw new ArgumentNullException(nameof(fileStream), "File stream cannot be null.");

            // Temporary path for the service account key file
            string serviceAccountKeyPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".json");
            var result = new Dictionary<string, object> { { "status", 0 } };

            try
            {
                // Step 1: Download the authentication file from AuthUrl
                Console.WriteLine($"Downloading AuthUrl: {AuthUrl}");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(AuthUrl);
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Failed to download the file from AuthUrl. Status code: {response.StatusCode}");

                    var fileBytes = await response.Content.ReadAsByteArrayAsync();
                    if (fileBytes == null || fileBytes.Length == 0)
                        throw new Exception("The downloaded service account key file is empty or null.");

                    // Save the file locally to the temporary path
                    await File.WriteAllBytesAsync(serviceAccountKeyPath, fileBytes);
                }

                // Step 2: Authenticate with Google Drive API
                GoogleCredential credential;
                using (var stream = new FileStream(serviceAccountKeyPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
                }

                // Step 3: Initialize the Google Drive API service
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = Authname,
                });

                // Step 4: Set up file metadata
                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = fileName,
                    Parents = new List<string> { FolderId }
                };

                // Step 5: Upload the file
                var request = service.Files.Create(fileMetadata, fileStream, "application/octet-stream");
                request.Fields = "id, webViewLink";

                var uploadResponse = await request.UploadAsync();

                if (uploadResponse.Status == UploadStatus.Completed)
                {
                    result["status"] = 1;
                    result["fileId"] = request.ResponseBody.Id;
                    result["webViewLink"] = request.ResponseBody.WebViewLink;
                }
                else
                {
                    result["message"] = $"File upload failed. Status: {uploadResponse.Status}";
                    if (uploadResponse.Exception != null)
                    {
                        result["errorDetails"] = uploadResponse.Exception.Message;
                        Console.WriteLine($"Error Details: {uploadResponse.Exception}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                result["message"] = $"Error: {ex.Message}";
                result["stackTrace"] = ex.StackTrace;
            }
            finally
            {
                // Step 6: Clean up the temporary file
                if (File.Exists(serviceAccountKeyPath))
                {
                    File.Delete(serviceAccountKeyPath);
                }
            }

            return result;
        }
    

    public static async Task<byte[]> DownloadFilePartsFromGoogleDriveByName(string fileNameBase, string encryptionKey,string AuthUrl,string AuthName,string folderID)
        {
            string localServiceAccountKeyPath = Path.GetTempFileName(); // Temporary file path

            try
            {
                // If ServiceAccountKeyPath is a URL, download the file
                if (AuthUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(AuthUrl);
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Failed to download the service account key. Status code: {response.StatusCode}");
                        }

                        var downloadedBytes = await response.Content.ReadAsByteArrayAsync();
                        await File.WriteAllBytesAsync(localServiceAccountKeyPath, downloadedBytes);
                    }
                }
                else
                {
                    // Use the local path directly if it's valid
                    localServiceAccountKeyPath = AuthUrl;
                }

                // Authenticate using the service account
                GoogleCredential credential;
                using (var stream = new FileStream(localServiceAccountKeyPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
                }

                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = AuthName,
                });

                int partIndex = 0;
                var decryptedParts = new List<byte[]>();

                // Download all parts
                while (true)
                {
                    try
                    {
                        // Construct part file name dynamically
                        string partFileName = $"{fileNameBase}.part{partIndex}";

                        // Search for the file using its name
                        var listRequest = service.Files.List();
                        listRequest.Q = $"name = '{partFileName}' and trashed = false";
                        listRequest.Fields = "files(id, name)";
                        var fileList = await listRequest.ExecuteAsync();

                        if (fileList.Files == null || fileList.Files.Count == 0)
                        {
                            // Break the loop if no more file parts are found
                            break;
                        }

                        // Get the file ID of the part
                        string partFileId = fileList.Files.First().Id;

                        // Request the file part from Google Drive
                        var request = service.Files.Get(partFileId);
                        using (var memoryStream = new MemoryStream())
                        {
                            await request.DownloadAsync(memoryStream);
                            byte[] encryptedPart = memoryStream.ToArray();

                            // Extract the key and IV
                            byte[] key = Encoding.UTF8.GetBytes(encryptionKey);
                            byte[] iv = encryptedPart.Take(16).ToArray();
                            byte[] encryptedData = encryptedPart.Skip(16).ToArray();

                            // Decrypt the file part
                            byte[] decryptedPart = DecryptData(encryptedData, key, iv);
                            decryptedParts.Add(decryptedPart);
                        }

                        partIndex++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error downloading file part '{fileNameBase}.part{partIndex}': {ex.Message}");
                        throw;
                    }
                }

                // Combine all parts
                byte[] fileBytes = CombineByteArrays(decryptedParts);
                return fileBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading or decrypting file parts: {ex.Message}");
                throw;
            }
            finally
            {
                // Clean up temporary files
                if (File.Exists(localServiceAccountKeyPath))
                {
                    File.Delete(localServiceAccountKeyPath);
                }
            }
        }




    }
}


