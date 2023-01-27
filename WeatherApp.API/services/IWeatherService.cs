using WeatherApp.API.Models;

namespace WeatherApp.API.services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherApi(double latitude, double longitude);
        Task<WeatherResponse> GetWeatherByCityName(string cityName);
        Task<WeatherResponse> GetWeatherApi2(double latitude, double longitude);
    }
}
