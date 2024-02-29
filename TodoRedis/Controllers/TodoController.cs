using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public TodoController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        // GET api/<TodoController>/499de4d6-46d2-4996-89b3-096726063a89
        [HttpGet("{key}")]
        public async Task<string?> GetAsync(string key)
        {
            return await _distributedCache.GetStringAsync(key);
        }

        // POST api/<TodoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var guid = Guid.NewGuid();
            _distributedCache.SetStringAsync(guid.ToString(), value);
        }

        // PUT api/<TodoController>/5
        [HttpPut("{key}")]
        public void Put(string key, [FromBody] string value)
        {
            _distributedCache.SetStringAsync(key,value);
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{key}")]
        public void Delete(string key)
        {
            _distributedCache.RemoveAsync(key);
        }
    }
}
