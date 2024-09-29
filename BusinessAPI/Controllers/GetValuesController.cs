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

        // GET api/<ValuesController>
        [HttpGet()]
        public IActionResult Get()
        {
            RestRequest request = new RestRequest("/data/get", Method.Get);
            RestResponse response = _client.Execute(request);
            return Ok(response);
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
                    if (!string.IsNullOrWhiteSpace(response.Content))
                    {
                        DataIntermed data = JsonConvert.DeserializeObject<DataIntermed>(response.Content);
                        if (data != null)
                        {
                            Console.WriteLine(data.ToString());
                            return Ok(data);
                        }
                    }
                    return NotFound($"No data found for index {index}");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Error: {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
