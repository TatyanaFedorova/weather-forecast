using WeatherForecast.Models;


namespace WeatherForecast.Interfaces
{
    /// <summary>
    /// Interface for City repository operations.
    /// </summary>
    public interface ICityRepository
    {
        /// <summary>
        /// Gets all cities.
        /// </summary>
        /// <returns>A list of cities.</returns>
        Task<IEnumerable<City>> GetAllAsync();

        /// <summary>
        /// Adds a new city.
        /// </summary>
        /// <param name="city">The city to add.</param>
        Task AddAsync(City city);
    }
}