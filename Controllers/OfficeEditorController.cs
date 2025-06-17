using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Drives.Item.Items.Item.CreateLink;
using Microsoft.Graph.Models;

namespace OISPublic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfficeEditorController : ControllerBase
    {
        private readonly GraphServiceClient _graphServiceClient;

        public OfficeEditorController(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Empty file.");

            using var stream = file.OpenReadStream();
            var user = await _graphServiceClient.Me.GetAsync();
            if(user is null)
                return Unauthorized("User not found.");

            var driveItem = await _graphServiceClient.Users[user.Id].Drive.GetAsync();

            if(driveItem is null)
                return StatusCode(500, "Failed to retrieve user's drive.");

            // Create a new file in the user's drive root
            var uploaded = await _graphServiceClient.Drives[driveItem.Id].Root.ItemWithPath(file.FileName).Content.PutAsync(stream);


            if (uploaded is null)
                return StatusCode(500, "Failed to upload file.");

            // ✅ Use correct request body type for link creation
            var linkRequest = new CreateLinkPostRequestBody
            {
                Type = "edit",
                Scope = "organization"
            };

            var result = await _graphServiceClient.Drives[driveItem.Id].Items[uploaded.Id]
                .CreateLink.PostAsync(linkRequest);


            return Ok(new { editUrl = result?.Link?.WebUrl });
        }
    }
}
