
using WeatherForecast.DTOs;

public static class WeatherEndpointsMapper
{
    public static void MapWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast/{cityKey}", async (string cityKey, IWeatherService weatherService) =>
        {
            try
            {
                var weatherForecast = await weatherService.GetWeatherForecastForCityAsync(cityKey);
                return weatherForecast is null ? Results.NotFound() : Results.Ok(weatherForecast);
            }
            catch (ArgumentException ex) { return Results.BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return Results.NotFound(ex.Message); }
            catch (Exception ex) { return Results.Problem(ex.Message); }

        });

        app.MapGet("/currentweather/{cityKey}", async (string cityKey, IWeatherService weatherService) =>
        {
            try
            {
                var weather = await weatherService.GetCurrentWeatherForCityAsync(cityKey); ;
                return weather is null ? Results.NotFound() : Results.Ok(weather);
            }
            catch (ArgumentException ex) { return Results.BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return Results.NotFound(ex.Message); }
            catch (Exception ex) { return Results.Problem(ex.Message); }
        });

        app.MapGet("/searchCityByName/{cityName}", async (string cityName, IWeatherService weatherService) =>
        {
            try
            {
                var cities = await weatherService.GetCitiesByNameAsync(cityName); ;
                return cities is null ? Results.NotFound() : Results.Ok(cities);
            }
            catch (ArgumentException ex) { return Results.BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return Results.NotFound(ex.Message); }
            catch (Exception ex) { return Results.Problem(ex.Message); }
        });
    }
}