using WeatherForecast.Services;
using WeatherForecast.Models;

namespace WeatherForecast.Mappings
{
    public static class CityEndpointsMapper
    {
        public static void MapCityEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/cities");
            group.MapGet("/", async (CityService cityService) =>
            {
                var cities = await cityService.GetAllCitiesAsync();
                return Results.Ok(cities);
            });

            group.MapGet("/{id}", async (int id, CityService cityService) =>
            {
                var city = await cityService.GetCityByIdAsync(id);
                return city is not null ? Results.Ok(city) : Results.NotFound();
            });

            group.MapPost("/", async (City city, CityService cityService) =>
            {
                await cityService.AddCityAsync(city);
                return Results.Created($"/api/cities/{city.Id}", city);
            });

            group.MapPut("/{id}", async (int id, City city, CityService cityService) =>
            {
                try
                {
                    await cityService.UpdateCityAsync(id, city);
                    return Results.NoContent();
                }
                catch (ArgumentException)
                {
                    return Results.BadRequest("City ID mismatch.");
                }

            });

            group.MapDelete("/{id}", async (int id, CityService cityService) =>
            {
                await cityService.DeleteCityAsync(id);
                return Results.NoContent();
            });

            group.MapGet("/getlatest", async (int? count, CityService cityService) =>
            {
                var cities = await cityService.GetLatestCitiesAsync(count);
                return Results.Ok(cities);
            });
        }
    }
}
