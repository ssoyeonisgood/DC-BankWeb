using APIClass;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GetValuesController : ControllerBase
    {
        private readonly RestClient _client;

        public GetValuesController()
        {
            _client = new RestClient("http://localhost:5247");
        }

        // GET api/<ValuesController>/5
        [HttpGet("{index}")]
        public async Task<IActionResult> Get(int index)
        {
            try
            {
                RestRequest request = new RestRequest("/api/data/{index}", Method.Get);
                request.AddUrlSegment("index", index);
                RestResponse response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var formattedResponse = JsonConvert.DeserializeObject<DataIntermed>(response.Content);
                    return Ok(formattedResponse);
                }

                return NotFound($"No data found for index {index}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("total")]
        public async Task<IActionResult> GetTotal()
        {
            try
            {
                RestRequest request = new RestRequest("api/data/total", Method.Get);

                // Execute the request asynchronously
                RestResponse response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return Ok(response.Content);
                }

                return StatusCode((int)response.StatusCode, "Failed to retrieve the total count.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
