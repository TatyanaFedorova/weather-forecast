using WeatherForecast.Interfaces;
using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public class CityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<City?> GetCityByIdAsync(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }

        public async Task AddCityAsync(City city)
        {
            await _cityRepository.AddAsync(city);
        }

        public async Task UpdateCityAsync(long id, City city)
        {
            if (id != city.Id)
            {
                throw new ArgumentException("City ID mismatch.");
            }
            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteCityAsync(int id)
        {
            await _cityRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<City>> GetLatestCitiesAsync(int? count = null)
        {
            return await _cityRepository.GetLatest(count);
        }
    }
}