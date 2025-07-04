using Microsoft.Extensions.Configuration;
using System;

namespace DocumentManagement.Helper
{
    public class PathHelper
    {
        public IConfiguration _configuration;

        public PathHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string DocumentPath
        {
            get
            {
                return _configuration["DocumentPath"];
            }
        }
        public string AesEncryptionKey
        {
            get
            {
                return _configuration["AesEncryptionKey"];
            }
        }
        public string IinitializationVector
        {
            get
            {
                return _configuration["IinitializationVector"];
            }
        }
        public bool AllowEncryption
        {
            get
            {
                return Convert.ToBoolean(_configuration["AllowEncryption"]);
            }
        }
        public string DocumentUrl
        {
            get
            {
                return _configuration["Url_ShareLink"];
            }
        }
        public string ReminderFromEmail
        {
            get
            {
                return _configuration["ReminderFromEmail"];
            }
        }
        public string DocUrl
        {
            get
            {
                return _configuration["DocUrl"];
            }
        }


        public string SSOUrl
        {
            get
            {
                return _configuration["SSOUrl"];
            }
        }

        public string SecretkeySSO
        {
            get
            {
                return _configuration["SecretkeySSO"];
            }
        }


        public string GoogleUrl
        {
            get
            {
                return _configuration["GoogleUrl"];
            }
        }
        public string  DMSUrl
        {
            get
            {
                return _configuration["DMSUrl"];
            }
        }
        public string WFMUrl
        {
            get
            {
                return _configuration["WFMUrl"];
            }
        }
        public string externalLinkChannelName
        {
            get
            {
                return _configuration["externalLinkChannel"];
            }
        }
        public string QueueName
        {
            get
            {
                return _configuration["QueueName"];
            }
        }
      
            public string PublicRedirectingURL
        {
            get
            {
                return _configuration["PublicRedirectingURL"];
            }
        }

        public string PublicDir
        {
            get
            {
                return _configuration["PublicDir"];
            }
        }
        public string baseUrl
        {
            get
            {
                return _configuration["baseUrl"];
            }
        }
        public string[] CorsUrls
        {
            get
            {
                return string.IsNullOrEmpty(_configuration["CorsUrls"]) ? new string[] { } : _configuration["CorsUrls"].Split(",");
            }
        }

        public string TempPath { get; set; }
    }
}
