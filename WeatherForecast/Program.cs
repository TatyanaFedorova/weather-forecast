using WeatherForecast.Data;
using WeatherForecast.Interfaces;
using WeatherForecast.Repositories;
using WeatherForecast.Mappings;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Services;
using Microsoft.AspNetCore.Http.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddSingleton<IWeatherService, WeatherService>();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen();

builder.Services.Configure<WeatherSettings>(builder.Configuration.GetSection("WeatherSettings"));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    });
});

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
CityEndpointsMapper.MapCityEndpoints(app);
WeatherEndpointsMapper.MapWeatherEndpoints(app);

app.Run();