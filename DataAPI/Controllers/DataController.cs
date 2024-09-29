using APIClasses;
using DataAPI.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly Database _db = Database.Instance;

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<DataIntermed> Get()
        {

            return _db.GetAllAccount(); 
        }

        // GET api/<ValuesController>/5
        [HttpGet("{index}")]
        public IActionResult Get(int index)
        {
            DataIntermed data = _db.GetAccountByIndex(index);
            Console.WriteLine("Data API:" + data.ToString());
            if (data == null)
            {
                return NotFound($"No data found for index {index}");
            }
            return new ObjectResult(data) { StatusCode = 200 };
        }

        // POST: api/data/search
        [HttpPost("search")]
        public IActionResult Search([FromBody] SearchData data)
        {
            DataIntermed? result = _db.GetAccountByLastName(data.searchString);

            Console.WriteLine("result: " + result.ToString());

            if (result == null)
            {
                return NotFound($"No data found for search term: {data.searchString}");
            }

            return Ok(result);
        }

        // GET: api/data/total
        [HttpGet("total")]
        public IActionResult Total()
        {
            int total = _db.GetNumRecords();
            Console.WriteLine(total);
            return new ObjectResult(total) { StatusCode = 200 };
        }
        

    }
}
