namespace WeatherForecast.Models
{
    public class City
    {
        public int Id { get; set; } = 0;
        public string LocationKey { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string AdministrativeArea { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}