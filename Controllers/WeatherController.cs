using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;

namespace WeatherApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var data = await _weatherService.GetWeatherAsync();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Не удалось получить данные о погоде", details = ex.Message });
        }
    }
}
