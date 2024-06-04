using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace test2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly ILogger<PhotosController> _logger;

        private readonly HttpClient _httpClient;

        public PhotosController(HttpClient httpClient, ILogger<PhotosController> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        [HttpPost("SavePhoto")]
        public async Task<IActionResult> SavePhoto([FromBody] PhotoDto photoDto)
        {
            if (photoDto == null || string.IsNullOrEmpty(photoDto.Photo))
            {
                return BadRequest("Invalid photo data.");
            }

            try
            {
                // Convert Base64 string to byte array
                byte[] imageBytes = Convert.FromBase64String(photoDto.Photo.Split(',')[1]);

                // Create image from byte array
                using (var ms = new MemoryStream(imageBytes))
                {
                    using (var image = Image.FromStream(ms))
                    {
                        // Generate unique file name
                        string fileName = $"room_{photoDto.RoomId}_{photoDto.VerifyType}_{DateTime.Now:yyyyMMddHHmmss}.png";

                        // Define the path to save the photo
                        string filePath = Path.Combine("wwwroot/images/checklists", fileName);

                        // Ensure the directory exists
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                        // Save the image to the file path
                        image.Save(filePath, ImageFormat.Png);

                        // Return the path to the saved image
                        return Ok(new { photoPath = $"/images/checklists/{fileName}" });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("GetImage")]
        public IActionResult GetImage([FromQuery] string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return BadRequest("Image name is required.");
            }

            try
            {
                string imagePath = Path.Combine("wwwroot/images/checklists", imageName);
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/png"); // Adjust the mime type based on your image format
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving image: {ex.Message}");
            }
        }
    }
    

        public class PhotoDto
        {
            public string Photo { get; set; } // Base64 encoded string
            public int RoomId { get; set; }
            public string VerifyType { get; set; }
        }
    
}
