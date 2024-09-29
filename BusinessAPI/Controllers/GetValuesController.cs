using APIClasses;
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
        public IActionResult Get(int index)
        {
            try
            {
                RestRequest request = new RestRequest("/api/data/{index}", Method.Get);
                request.AddUrlSegment("index", index);
                RestResponse response = _client.Execute(request);

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
        public IActionResult GetTotal()
        {
            RestRequest request = new RestRequest("api/data/total", Method.Get);
            RestResponse response = _client.Execute(request);
            return Ok(response.Content);

            //if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
            //{
            //    int totalAccounts = JsonConvert.DeserializeObject<int>(response.Content);

            //    return Ok(totalAccounts);
            //}
            //return StatusCode((int)response.StatusCode, $"Error: {response.ErrorMessage}");
        }
    }
}
