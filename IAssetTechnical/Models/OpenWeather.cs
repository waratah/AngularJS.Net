using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAssetTechnical.Models
{
    //  Note mapping of this class tested in GetJsonOpenWeather

    public class Sys
    {
        public string Country { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }

    public class Coord {
        public decimal Lon { get; set; }
        public decimal Lat { get; set; }
    }
    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Main
    {
        public decimal Temp { get; set; }
        public decimal Humidity { get; set; }
        public decimal Pressure { get; set; }
        public decimal Temp_min { get; set; }
        public decimal Temp_max { get; set; }
    }

    public class Wind
    {
        public decimal Speed { get; set; }
        public decimal Deg { get; set; }
    }

    public class Clouds
    {
        public decimal All { get; set; }
    }

    public class OpenWeather
    {
        public Coord Coord { get; set; }
        public Sys Sys { get; set; }
        public List<Weather> Weather { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds{ get; set; }
        public int Dt { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }
}


