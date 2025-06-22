public record CurrentWeatherDTO
{
    public string Description { get; init; } = string.Empty;
    public double Temperature { get; init; }
}
