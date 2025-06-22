namespace WeatherForecast.DTOs;

public record WeatherForecastDTO
{
    public string Description { get; init; } = string.Empty;
    public double MinTemperature { get; init; }
    public double MaxTemperature { get; init; }
    public string DayDescription { get; init; } = string.Empty;
    public string NightDescription { get; init; } = string.Empty;
}
