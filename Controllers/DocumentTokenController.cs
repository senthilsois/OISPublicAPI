using DocumentManagement.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OISPublic.OISDataRoom;
using OISPublic.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OISPublic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentTokenController : ControllerBase
    {
        private readonly OISDataRoomContext _context;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly PathHelper _pathHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DocumentTokenController(
            IWebHostEnvironment webHostEnvironment,
            OISDataRoomContext context,
            IConfiguration configuration,
            JwtTokenService jwtTokenService,
            PathHelper pathHelper)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
            _pathHelper = pathHelper;
        }

        [HttpGet("{id}/token")]
        [AllowAnonymous]
        public async Task<ActionResult> GetDocumentToken(Guid id)
        {
            var documentToken = await _context.DocumentTokens
                .FirstOrDefaultAsync(dt => dt.DocumentId == id);

            Guid token;

            if (documentToken == null)
            {
                token = Guid.NewGuid();

                var newToken = new DocumentToken
                {
                    CreatedDate = DateTime.UtcNow,
                    DocumentId = id,
                    Token = token
                };

                _context.DocumentTokens.Add(newToken);
                await _context.SaveChangesAsync();
            }
            else
            {
                token = documentToken.Token;
            }

            return Ok(new { token = token.ToString() });
        }

        [HttpGet("{id}/url")]
        [AllowAnonymous]
        public async Task<ActionResult> GetDocumentUrl(Guid id)
        {
            var document = await _context.Documents
                .FirstOrDefaultAsync(d => d.Id == id);

            if (document == null)
            {
                return NotFound("Document not found.");
            }

            string documentPath = document.Url;

            if (document.CloudId != null)
            {
                var bucketInfo = await _context.AwsBuckets
                    .FirstOrDefaultAsync(b => b.Id == document.CloudId);

                if (bucketInfo == null)
                {
                    return NotFound("AWS bucket info not found.");
                }

                var cloudUrl = Path.Combine(bucketInfo.Url, documentPath);
                return Ok(new { url = cloudUrl });
            }

            var localUrl = Path.Combine(_pathHelper.DocumentPath, documentPath);
            return Ok(new { url = localUrl });
        }
    }
}
