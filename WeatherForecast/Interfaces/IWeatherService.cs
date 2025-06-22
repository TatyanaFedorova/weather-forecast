using WeatherForecast.DTOs;

public interface IWeatherService
{
    Task<WeatherForecastDTO?> GetWeatherForecastForCityAsync(string cityKey);
    Task<CurrentWeatherDTO?> GetCurrentWeatherForCityAsync(string cityKey);
    Task<IEnumerable<CityDTO>?> GetCitiesByNameAsync(string cityName);
    //Task<CityDTO> GetCityByKeyAsync(string cityKey);
}
