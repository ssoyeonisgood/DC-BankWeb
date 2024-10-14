using APIClass;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly RestClient _client;

        public SearchController()
        {
            _client = new RestClient("http://localhost:5247");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SearchData data)
        {
            RestRequest request = new RestRequest("api/data/search", Method.Post);
            request.AddJsonBody(data);
            RestResponse response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                DataIntermed? result = JsonConvert.DeserializeObject<DataIntermed>(response.Content);

                if (result == null)
                {
                    return StatusCode((int)response.StatusCode, "Failed to deserialize response content.");
                }

                return Ok(result);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }
        }

    }
}
