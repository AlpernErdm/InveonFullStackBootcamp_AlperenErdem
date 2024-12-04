using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisEntegration.Interfaces;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IRedisCacheService _redisCacheService;

        public CacheController(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        [HttpGet("/cache/{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var value = await _redisCacheService.GetValueAsync(key);
            if (string.IsNullOrEmpty(value))
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost("/cache/set")]
        public async Task<IActionResult> Set([FromBody] RedisCacheRequestModel redisCacheRequestModel)
        {
            var result = await _redisCacheService.SetValueAsync(redisCacheRequestModel.Key, redisCacheRequestModel.Value);
            if (result)
            {
                return Ok();
            }
            return StatusCode(500, "Cache set operation failed.");
        }

        [HttpDelete("/cache/{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            await _redisCacheService.Clear(key);
            return Ok();
        }
    }

    public class RedisCacheRequestModel
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}