namespace WeatherApp.API.Models
{
    public class Models { 
    public class CurrentAirPollutionData
    {
        public Coord coord { get; set; }
        public List[] list { get; set; }
    }

    public class Coord
    {
        public int lon { get; set; }
        public int lat { get; set; }
    }

    public class List
    {
        public Main main { get; set; }
        public Components components { get; set; }
        public int dt { get; set; }
    }

    public class Main
    {
        public int aqi { get; set; }
    }

    public class Components
    {
        public float co { get; set; }
        public int no { get; set; }
        public float no2 { get; set; }
        public float o3 { get; set; }
        public float so2 { get; set; }
        public float pm2_5 { get; set; }
        public float pm10 { get; set; }
        public float nh3 { get; set; }
    }

    }

}
