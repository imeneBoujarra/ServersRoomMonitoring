using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Test.Data;
using Test.Data.Models;

namespace test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricalController : ControllerBase
    {
        private readonly AppDb_Context _db;

        public HistoricalController(AppDb_Context db)
        {
            _db = db;
        }

        // POST: api/Historical/UploadUrls

        [HttpPost]
        [Route("UploadUrls")]

        public async Task<IActionResult> UploadUrls([FromBody] UrlUploadModel model)
        {
            try
            {
                // Validate model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Store URLs in the database
                var historicalRecord = new Historical
                {
                    Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    Hour = DateTime.UtcNow.ToString("HH:mm:ss"),
                    Id_user = model.UserId,
                    PicturesFolderPath = model.PicturesFolderPath
                };
                _db.Historical.Add(historicalRecord);
                await _db.SaveChangesAsync();

                return Ok("URLs uploaded successfully.");
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error uploading URLs: {ex.Message}");
                return StatusCode(500, "An error occurred while uploading URLs.");
            }
        }
        /*
                // POST: api/Historical/UploadImages
                [HttpPost("UploadImages")]
                public async Task<IActionResult> UploadImages([FromForm] ImageUploadModel model)
                {
                    try
                    {
                        // Validate model and extract image URLs
                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }

                        // Store URLs in the database
                        foreach (var imageUrl in model.ImageUrls)
                        {
                            var image = new Image
                            {
                                Url = imageUrl,
                                UploadedAt = DateTime.UtcNow
                            };
                            _db.Images.Add(image);
                        }
                        await _db.SaveChangesAsync();

                        return Ok("Images uploaded successfully.");
                    }
                    catch (Exception ex)
                    {
                        // Log the error
                        Console.WriteLine($"Error uploading images: {ex.Message}");
                        return StatusCode(500, "An error occurred while uploading images.");
                    }
                }
            }*/

        public class ImageUploadModel
        {
            public string[] ImageUrls { get; set; }
        }
        public class UrlUploadModel
        {
            public int UserId { get; set; } // User ID associated with the URLs
            public string PicturesFolderPath { get; set; } // Folder path containing pictures
        }
    }
}
