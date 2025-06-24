public interface ICityService
    {
        Task AddCityAsync(CityDTO cityDTO);
        Task<IEnumerable<CityDTO>?> GetAllCitiesAsync();
    }