using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Mappings
{
    public static class CityEndpointsMapper
    {
        public static void MapCityEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/cities");
            group.MapGet("/", async ([FromServices] ICityService cityService) =>
            {
                try
                {
                    var cities = await cityService.GetAllCitiesAsync();
                    return Results.Ok(cities);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
                catch (Exception)
                {
                    // Log the exception if needed
                    return Results.Problem("An unexpected error occurred while retrieving cities.");
                }
            });

            group.MapPost("/", async (CityDTO cityDTO, [FromServices] ICityService cityService) =>
            {
                await cityService.AddCityAsync(cityDTO);
                return Results.Created();
            });
        }
    }
}
