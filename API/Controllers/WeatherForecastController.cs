using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

// [ApiController] là bộ điều khiển API
[ApiController]
// Router cho bộ điều khiển API 
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

// ILogger<WeatherForecastController> là một loại của biến _logger
// Trong TH này, biến _logger là 1 đối tượng của một loại gioa diện (interface) được gọi là ILogger<T>
// Ở đây sẽ ghi lại thông tin liên quan đến WeatherForecastController
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    // Khi gửi yêu cầu lên trình duyệt, sever sẽ nhận HTTP cho API, sau đó trả về 1 mã dưới dạng JSON 
    // về trình duyệt 
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
}
