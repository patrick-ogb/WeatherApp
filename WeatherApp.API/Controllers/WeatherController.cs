using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WeatherApp.API.services;
using WeatherApp.API.ViewModels.Authentication;

namespace WeatherApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [Authorize(Roles = $"{UserRoles.Admin}")]
        [HttpGet("Current_weather_data")]
        public async Task<IActionResult> GetWeatherApi(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
                return BadRequest(new { latitude, longitude });

            var weather = await _weatherService.GetWeatherApi(latitude, longitude);

            return Ok(weather.currentWeather);
        }

        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Tester}")]
        [HttpGet("Call_5_day_3_hour_forecastdata")]
        public async Task<IActionResult> GetWeatherByCityName(string cityName)
        {
            if (cityName is null)
                return BadRequest(cityName);

            var weather = await _weatherService.GetWeatherByCityName(cityName);

            return Ok(weather.currentWeather);
        }

        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
        [HttpGet("Air_pollution")]
        public async Task<IActionResult> AirPollution(int lattitude, int longitude)
        {
            if (lattitude < 0 || longitude < 0)
                return BadRequest(new { lattitude, longitude });

            var weather = await _weatherService.AirPolution(lattitude, longitude);

            return Ok(weather.CurrentAirPollutionData);
        }

    }
}
