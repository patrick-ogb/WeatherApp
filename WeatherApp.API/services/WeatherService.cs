using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WeatherApp.API.Models;
using static WeatherApp.API.Models.Models;

namespace WeatherApp.API.services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<WeatherResponse> GetWeatherApi(double latitude, double longitude)
        {
            _httpClient.BaseAddress = new Uri(_configuration["WeatherSettings:BaseUrl"]);
            var response = await _httpClient.GetAsync($"weather?lat={latitude}&lon={longitude}&appid={_configuration["WeatherSettings:ApiKey"]}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await System.Text.Json.JsonSerializer.DeserializeAsync<CurrentWeather>(responseStream);

            return new WeatherResponse { currentWeather = responseObject, IsSuccess = true};
        }
        
        public async Task<WeatherResponse> GetWeatherByCityName(string cityName)
        {
            _httpClient.BaseAddress = new Uri(_configuration["WeatherSettings:BaseUrl"]);
            var response = await _httpClient.GetAsync($"weather?q={cityName}&units=metric&cnt=1&APPID={_configuration["WeatherSettings:ApiKey"]}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await System.Text.Json.JsonSerializer.DeserializeAsync<CurrentWeather>(responseStream);

            return new WeatherResponse { currentWeather = responseObject, IsSuccess = true};
        }
        
        public async Task<WeatherResponse> AirPolution(int lat, int lon)
        {
            _httpClient.BaseAddress = new Uri(_configuration["WeatherSettings:BaseUrl"]);
            var response = await _httpClient.GetAsync($"air_pollution?lat={lat}&lon={lon}&appid={_configuration["WeatherSettings:ApiKey"]}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await System.Text.Json.JsonSerializer.DeserializeAsync<CurrentAirPollutionData>(responseStream);

            return new WeatherResponse { CurrentAirPollutionData = responseObject, IsSuccess = true};
        }


    }
}
