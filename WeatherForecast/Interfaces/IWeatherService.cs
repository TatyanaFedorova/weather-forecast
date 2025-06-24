public interface IWeatherService
{
    Task<CurrentWeatherDTO?> GetCurrentWeatherForCityAsync(string cityKey);
    Task<IEnumerable<CityDTO>?> GetCitiesByNameAsync(string cityName);
}
