using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace WeatherForecast.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly IMemoryCache _memoryCache;
        private readonly IOptions<WeatherSettings> _settings;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(HttpClient httpClient,
         IMemoryCache memoryCache,
         IOptions<WeatherSettings> settings,
         ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
            _settings = settings;
            _logger = logger;

            _apiKey = settings.Value.ApiKey ??
                throw new InvalidOperationException("Weather API key is not configured.");
            _baseUrl = settings.Value.BaseUrl ??
                throw new InvalidOperationException("Weather API base URL is not configured.");

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<IEnumerable<CityDTO>?> GetCitiesByNameAsync(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                throw new ArgumentException("City name is required", nameof(cityName));

            var cacheKey = $"LocationKey:{cityName}";
            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<CityDTO>? cachedCities))
                return cachedCities;

            var requestUri = $"{_baseUrl}/locations/v1/cities/search?apikey={_apiKey}&q={Uri.EscapeDataString(cityName)}";
            var response = await ExecuteWithRetryAsync(() => _httpClient.GetAsync(requestUri));

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get any city for {CityName}. Status: {StatusCode}", cityName, response.StatusCode);
                throw new HttpRequestException($"Failed to get any city key: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();

            if(string.IsNullOrEmpty(content))
                return null;

            var data = JsonSerializer.Deserialize<IEnumerable<City>>(content, _jsonSerializerOptions);
            if (data is null)
                return null;

            var results = data.Select(city => new CityDTO
            {
                LocationKey = city.Key,
                Name = city.EnglishName,
                Country = city.Country?.EnglishName ?? string.Empty,
                AdministrativeArea = city.AdministrativeArea?.EnglishName ?? string.Empty,
                Rank = city.Rank != 0 ? city.Rank : int.MaxValue
            })
            .OrderBy(city => city.Rank)
            .ToList();

            _memoryCache.Set(cacheKey, results, TimeSpan.FromHours(1));

            return results;
        }
        public async Task<CurrentWeatherDTO?> GetCurrentWeatherForCityAsync(string cityKey)
        {
            if (string.IsNullOrEmpty(cityKey))
            {
                throw new ArgumentNullException("City Key is null of empty");
            }

            string cacheKey = $"currentweather_{cityKey}";
            if (_memoryCache.TryGetValue(cacheKey, out CurrentWeatherDTO? cachedWeather) && cachedWeather != null)
                return cachedWeather;

            var requestUri = $"{_baseUrl}/currentconditions/v1/{cityKey}?apikey={_apiKey}";
            var response = await ExecuteWithRetryAsync(() => _httpClient.GetAsync(requestUri));
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Weather API failed: {StatusCode}", response.StatusCode);
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
            {
                _logger.LogError("Received empty response from weather API.");
                throw new InvalidOperationException("Received empty response from weather API.");
            }

            var currentConditions = JsonSerializer
            .Deserialize<List<CurrentWeatherResult>>(content, _jsonSerializerOptions)?
            .FirstOrDefault();

            if (currentConditions is null)
            {
                _logger.LogError($"Weather data missing in API response for location key: {cityKey}");
                throw new InvalidOperationException($"Weather data missing in API response for location key: {cityKey}");
            }

            var dto = new CurrentWeatherDTO
            {
                Description = currentConditions.WeatherText,
                Temperature = currentConditions.Temperature?.Metric?.Value ?? 0
            };

            _memoryCache.Set(cacheKey, dto, TimeSpan.FromMinutes(_settings.Value.WeatherMinutes));

            return dto;
        }

        private async Task<HttpResponseMessage> ExecuteWithRetryAsync(Func<Task<HttpResponseMessage>> operation)
        {
            const int maxRetries = 3;
            const int delayMilliseconds = 500;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    var response = await operation();
                    if (response.IsSuccessStatusCode)
                        return response;

                    if (attempt == maxRetries)
                        return response;
                }
                catch when (attempt < maxRetries) { }

                await Task.Delay(delayMilliseconds * attempt); // exponential backoff
            }

            throw new HttpRequestException("Failed after retries.");
        }

        #region CurrentWetherDataStructures
        private record CurrentWeatherResult
        {

            [JsonPropertyName("WeatherText")]
            public string WeatherText { get; init; } = string.Empty;

            [JsonPropertyName("Temperature")]
            public Temperature Temperature { get; init; } = new Temperature();
        }

         private record Temperature
        {
            [JsonPropertyName("Metric")]
            public TemperatureDetails? Metric { get; init; }
        }

        private record TemperatureDetails
        {
            [JsonPropertyName("Value")]
            public double Value { get; init; }

            [JsonPropertyName("Unit")]
            public string Unit { get; init; } = string.Empty;

            [JsonPropertyName("UnitType")]
            public int UnitType { get; init; }
        }
        #endregion
        #region CityDataStructures
        private record City
        {
            public string Key { get; init; } = string.Empty;
            public string EnglishName { get; init; } = string.Empty;
            public int Rank { get; init; }
            public Country? Country { get; init; }

            public AdministrativeArea? AdministrativeArea { get; init; }
        }
        private record Country
        {
            public string EnglishName { get; set; } = string.Empty;
        }

        private record AdministrativeArea
        {
            public string EnglishName { get; set; } = string.Empty;
        }
        #endregion
    }
}