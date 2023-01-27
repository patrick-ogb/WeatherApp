using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using WeatherApp.API.Entities;
using WeatherApp.API.Models;

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
            var responseObject = await JsonSerializer.DeserializeAsync<CurrentWeather>(responseStream);

            return new WeatherResponse { currentWeather = responseObject, IsSuccess = true};
        }
        
        public async Task<WeatherResponse> GetWeatherByCityName(string cityName)
        {
            _httpClient.BaseAddress = new Uri(_configuration["WeatherSettings:BaseUrl"]);
            var response = await _httpClient.GetAsync($"weather?q={cityName}&units=metric&cnt=1&APPID={_configuration["WeatherSettings:ApiKey"]}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<CurrentWeather>(responseStream);

            return new WeatherResponse { currentWeather = responseObject, IsSuccess = true};
        }
        
        public async Task<WeatherResponse> GetWeatherApi2(double latitude, double longitude)
        {
            _httpClient.BaseAddress = new Uri(_configuration["WeatherSettings:BaseUrl"]);
            var response = await _httpClient.GetAsync($"weather?lat={latitude}&lon={longitude}&appid={_configuration["WeatherSettings:ApiKey"]}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<CurrentWeather>(responseStream);

            return new WeatherResponse { currentWeather = responseObject, IsSuccess = true};
        }


    }
}
