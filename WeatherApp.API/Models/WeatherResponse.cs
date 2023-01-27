using WeatherApp.API.Entities;

namespace WeatherApp.API.Models
{
    public class WeatherResponse
    {
        public CurrentWeather currentWeather { get; set; }
        public CurrentAirPollutionData CurrentAirPollutionData { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
