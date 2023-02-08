namespace WheatherForcast
{
    public class WheatherInfo
    {
        public string Cod { get; set; }
        public int Message { get; set; }
        public int Cnt { get; set; }
        public List[] List { get; set; }
        public City City { get; set; }

    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public int Timezone { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }

    public class Coord
    {
        public float Lat { get; set; }
        public float Lon { get; set; }
    }

    public class List
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public Weather[] Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public int Visibility { get; set; }
        public double Pop { get; set; }
        public Sys Sys { get; set; }
        public string Dt_txt { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
        public float Feels_like { get; set; }
        public float Temp_min { get; set; }
        public float Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Sea_level { get; set; }
        public int Grnd_level { get; set; }
        public int Humidity { get; set; }
        public float Temp_kf { get; set; }
    }

    public class Clouds
    {
        public int All { get; set; }
    }

    public class Wind
    {
        public float Speed { get; set; }
        public int Deg { get; set; }
        public float Gust { get; set; }
    }

    public class Sys
    {
        public string Pod { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
