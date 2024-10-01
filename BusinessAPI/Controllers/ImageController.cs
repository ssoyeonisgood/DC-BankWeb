using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly string _imagePath = @"C:\Users\rnstk\OneDrive\Pictures\Screenshots\Screenshot 2024-09-27 135248.png"; // Update to your image file path

        [HttpGet("getimage")]
        public async Task<IActionResult> GetImage()
        {
            try
            {
                // Ensure the file exists
                if (!System.IO.File.Exists(_imagePath))
                {
                    return NotFound("Image not found.");
                }

                // Read the file as a stream and return it
                var image = System.IO.File.OpenRead(_imagePath);
                return File(image, "image/jpeg"); // Change "image/jpeg" to the appropriate mime type for your image
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error retrieving image: {ex.Message}");
            }
        }
    }
}
