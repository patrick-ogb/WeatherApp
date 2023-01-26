using WeatherApp.API.Models;

namespace WeatherApp.API.services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherApi(double latitude, double longitude);
        Task<WeatherResponse> GetWeatherByCityName(string cityName);
        Task<WeatherResponse> AirPolution(int lat, int lon);
    }
}
