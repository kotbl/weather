using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

public interface IWeatherService
{
    Task<ForecastResponse> GetWeatherAsync();
}

public class WeatherService : IWeatherService
{
    private const string ApiKey = "fa8b3df74d4042b9aa7135114252304";
    private const string Location = "55.7558,37.6173"; // Москва
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://api.weatherapi.com/v1/");
    }

    public async Task<ForecastResponse> GetWeatherAsync()
    {
        var url = $"forecast.json?key={ApiKey}&q={Location}&days=3&lang=ru";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ForecastResponse>(json)
               ?? throw new InvalidOperationException("Не удалось десериализовать данные о погоде");
    }
}
