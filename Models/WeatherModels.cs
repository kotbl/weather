using System.Text.Json.Serialization;

namespace WeatherApp.Models;

// Модели ответа от weatherapi.com
public class ForecastResponse
{
    [JsonPropertyName("location")]
    public Location Location { get; set; } = new();

    [JsonPropertyName("current")]
    public Current Current { get; set; } = new();

    [JsonPropertyName("forecast")]
    public Forecast Forecast { get; set; } = new();
}

public class Location
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("country")]
    public string Country { get; set; } = "";

    [JsonPropertyName("localtime")]
    public string Localtime { get; set; } = "";
}

public class Current
{
    [JsonPropertyName("temp_c")]
    public decimal TempC { get; set; }

    [JsonPropertyName("feelslike_c")]
    public decimal FeelslikeC { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("wind_kph")]
    public decimal WindKph { get; set; }

    [JsonPropertyName("wind_dir")]
    public string WindDir { get; set; } = "";

    [JsonPropertyName("pressure_mb")]
    public decimal PressureMb { get; set; }

    [JsonPropertyName("uv")]
    public decimal Uv { get; set; }

    [JsonPropertyName("condition")]
    public Condition Condition { get; set; } = new();
}

public class Condition
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = "";

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = "";
}

public class Forecast
{
    [JsonPropertyName("forecastday")]
    public List<ForecastDay> ForecastDay { get; set; } = [];
}

public class ForecastDay
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = "";

    [JsonPropertyName("day")]
    public Day Day { get; set; } = new();

    [JsonPropertyName("hour")]
    public List<Hour> Hour { get; set; } = [];
}

public class Day
{
    [JsonPropertyName("maxtemp_c")]
    public decimal MaxtempC { get; set; }

    [JsonPropertyName("mintemp_c")]
    public decimal MintempC { get; set; }

    [JsonPropertyName("avghumidity")]
    public int AvgHumidity { get; set; }

    [JsonPropertyName("maxwind_kph")]
    public decimal MaxWindKph { get; set; }

    [JsonPropertyName("daily_chance_of_rain")]
    public int DailyChanceOfRain { get; set; }

    [JsonPropertyName("condition")]
    public Condition Condition { get; set; } = new();
}

public class Hour
{
    [JsonPropertyName("time")]
    public string Time { get; set; } = "";

    [JsonPropertyName("time_epoch")]
    public long TimeEpoch { get; set; }

    [JsonPropertyName("temp_c")]
    public decimal TempC { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("wind_kph")]
    public decimal WindKph { get; set; }

    [JsonPropertyName("chance_of_rain")]
    public int ChanceOfRain { get; set; }

    [JsonPropertyName("condition")]
    public Condition Condition { get; set; } = new();
}
