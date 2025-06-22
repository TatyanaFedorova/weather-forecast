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
        /// Gets a city by its ID.
        /// </summary>
        /// <param name="id">The ID of the city.</param>
        /// <returns>The city with the specified ID.</returns>
        Task<City?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new city.
        /// </summary>
        /// <param name="city">The city to add.</param>
        Task AddAsync(City city);

        /// <summary>
        /// Updates an existing city.
        /// </summary>
        /// <param name="city">The city with updated information.</param>
        Task UpdateAsync(City city);

        /// <summary>
        /// Deletes a city by its ID.
        /// </summary>
        /// <param name="id">The ID of the city to delete.</param>
        Task DeleteAsync(int id);

        public Task<IEnumerable<City>> GetLatest(int? count = null);
    }
}