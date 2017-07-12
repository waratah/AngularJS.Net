using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

using IAssetTechnical.Interfaces;
using IAssetTechnical.Services;
using IAssetTechnical.Models;

namespace IAssetTechnical.Controllers
{

    public class WeatherController : ApiController
    {
        const string ApiKey = "3e1d60a13a5345cc9842b7ee4e029b8d";

        IGet<string> _getter;
        IGet<OpenWeather> _openGetter;

        public WeatherController()
        {
            var get = new XmlGet<string>
            {
                CleanUpString = " xmlns=\"http://www.webserviceX.NET\""
            };
            _getter = get;

            _openGetter = new JsonGet<OpenWeather>();
        }

        public WeatherController(IGet<string> getter, IGet<OpenWeather> openGetter)
        {
            _getter = getter;
            _openGetter = openGetter;
        }

        // GET api/values
        public WeatherResult Get(string country, string city)
        {
            // would no do this in production...  just a cheap hack to get this done for now...
            try
            {
                return GetWebservicex(country, city);
            }
            catch
            {
                return GetOpenWeather(country, city);
            }
        }


        private WeatherResult GetWebservicex(string country, string city)
        {
            var url = string.Format("http://www.webservicex.net/globalweather.asmx/GetWeather?CountryName={0}&CityName={1}", country, city);
            var data = _getter.Get(url);

            // may be "Data Not Found" will crash... this is a shortcut.
            var xml = XDocument.Parse(data);

            //var result = xml
            //    .Descendants("City")
            //    .Select(x => x.Value.Trim())
            //    .OrderBy(x => x);

            WeatherResult result = new WeatherResult
            {
                DewPoint = "",
                //Location = string.Format("Longitude {0}, Latitude {1}", data.Coord.Lon, data.Coord.Lat),
                //Pressure = data.Main.Pressure.ToString(),
                //RelativeHumidity = data.Main.Humidity.ToString(),
                //SkyConditions = conditions,
                //Temperature = data.Main.Temp.ToString(),
                Time = DateTime.Now.ToShortTimeString(),
                Visibility = "",
                //Wind = String.Format("Strength {0} from {1} degrees", data.Wind.Speed, data.Wind.Deg)
            };


            return result;
        }

        private WeatherResult GetOpenWeather(string country, string city)
        {
            var data = _openGetter.Get(string.Format("http://api.openweathermap.org/data/2.5/weather?q={1}&APPID={2}&units=metric", country, city, ApiKey));

            if (data == null)
                return null;

            var conditions = "";
            if(data.Weather.Any())
            {
                conditions = data.Weather.First().Description;
            }
            WeatherResult result = new WeatherResult
            {
                DewPoint = "",
                Location = string.Format("Longitude {0}, Latitude {1}", data.Coord.Lon, data.Coord.Lat),
                Pressure = data.Main.Pressure.ToString(),
                RelativeHumidity = data.Main.Humidity.ToString(),
                SkyConditions = conditions,
                Temperature = string.Format("{0} degrees C",data.Main.Temp),
                Time = DateTime.Now.ToShortTimeString(),
                Visibility = "",
                Wind = String.Format( "Strength {0} from {1} degrees", data.Wind.Speed, data.Wind.Deg)
            };
            return result;
        }
    }
}
