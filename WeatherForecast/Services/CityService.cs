using WeatherForecast.Interfaces;
using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<CityDTO>?> GetAllCitiesAsync()
        {
            var cities = await _cityRepository.GetAllAsync();
            var cityDTOs = cities?.Select(c => new CityDTO()
            {
                LocationKey = c.LocationKey,
                Name = c.Name,
                Country = c.Country,
                AdministrativeArea = c.AdministrativeArea,
                Rank = c.Rank
            }).ToList();

            if (cityDTOs is null) return null;
            return cityDTOs;
        }

        public async Task AddCityAsync(CityDTO cityDTO)
        {
            await _cityRepository.AddAsync(new City()
            {
                AdministrativeArea = cityDTO.AdministrativeArea,
                Country = cityDTO.Country,
                LocationKey = cityDTO.LocationKey,
                Name = cityDTO.Name,
                Rank = cityDTO.Rank
            });
        }
    }
}