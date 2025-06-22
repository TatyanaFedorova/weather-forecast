using WeatherForecast.Data;
using WeatherForecast.Interfaces;
using WeatherForecast.Models;
using Microsoft.EntityFrameworkCore;

namespace WeatherForecast.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly WeatherDbContext _context;

        public CityRepository(WeatherDbContext context)
        {
            _context = context;
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task AddAsync(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException(nameof(city), "City cannot be null.");
            }
            city.LastUpdated = DateTime.UtcNow; // Set the last updated time
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(City city)
        {
            city.LastUpdated = DateTime.UtcNow; // Update the last updated time
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var city = await GetByIdAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<City>> GetLatest(int? count = null)
        {

            var latestUpdatedCities = _context.Cities.OrderByDescending(c => c.LastUpdated);
            if (count.HasValue)
            {
                return await latestUpdatedCities.Take(count.Value).ToListAsync();
            }

            return await latestUpdatedCities.ToListAsync();
        }
    }
}