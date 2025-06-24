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

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _context.Cities
            .OrderByDescending(c => c.LastUpdated)
            .ToListAsync();
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
    }
}