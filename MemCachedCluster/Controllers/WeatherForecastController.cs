using Enyim.Caching;
using Enyim.Caching.Memcached;
using Microsoft.AspNetCore.Mvc;

namespace MemCachedCluster.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMemcachedClient _memcachedClient;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemcachedClient memcachedClient)
    {
        _logger = logger;
        _memcachedClient = memcachedClient;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
    
    [HttpGet("/cache", Name = "GetWeatherForecastFromCache")]
    public async Task<IActionResult> GetFromCache()
    {
        var result = await _memcachedClient.GetAsync<int>("WeatherForecast2");
        if (result.Success) return Ok(new { fromCache = result.Value});
        var t = 200;
        var deuBom = await _memcachedClient.SetAsync("WeatherForecast2", t, TimeSpan.Zero);
        return Ok(new { fromNoCache = t, deuBom });
    }
}