using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using IAssetTechnical;
using IAssetTechnical.Controllers;
using IAssetTechnical.Models;
using IAssetTechnical.Interfaces;

namespace IAssetTechnical.Tests.Controllers
{
    [TestClass]
    public class WeatherControllerTest
    {
        // cannot get any cities to work...
        [Ignore]
        [TestMethod]
        public void GetWeatherX()
        {
            //OpenWeather dict = null;

            Mock<IGet<String>> getter = new Mock<IGet<string>>();
            getter.Setup(x => x.Get(It.IsAny<string>())).Returns("No data");

            Mock<IGet<OpenWeather>> openGetter = new Mock<IGet<OpenWeather>>();
            //openGetter.Setup(x => x.Get(It.IsAny<string>())).Returns(dict);

            // Arrange
            var controller = new WeatherController(getter.Object, openGetter.Object);

            // Act
            var result = controller.Get("Country", "City");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull( result.Time);
            Assert.AreEqual("Longitude 139, Latitude 35", result.Location);
            Assert.AreEqual("", result.DewPoint);
            Assert.AreEqual("1013", result.Pressure);
            Assert.AreEqual("89", result.RelativeHumidity);
            Assert.AreEqual("overcast clouds", result.SkyConditions);
            Assert.AreEqual("289.5", result.Temperature);
            Assert.AreEqual("", result.Visibility);
            Assert.AreEqual("Strength 7.31 from 187.002 degrees", result.Wind);
        }


        [TestMethod]
        public void GetOpenWeather()
        {
            var weather = new OpenWeather
            {
                Coord = new Coord { Lon = 139.2M, Lat = 35.2M },
                Sys = new Sys { Country = "JP", Sunrise = 1369769524, Sunset = 1369821049 },
                Weather = new List<Weather> { new Weather { Id = 804, Main = "clouds", Description = "overcast clouds", Icon = "04n" } },
                Main = new Main { Temp = 289.5M, Humidity = 89, Pressure = 1013, Temp_min = 287.04M, Temp_max = 292.04M },
                Wind = new Wind { Speed = 7.31M, Deg = 187.002M },
                //Rain={"3h":0},
                //Clouds={"all":92},
                Dt = 1369824698,
                Id = 1851632,
                Name = "Shuzenji",
                Cod = 200
            };


            Mock<IGet<string>> getter = new Mock<IGet<string>>();
            getter.Setup(x => x.Get(It.IsAny<string>())).Throws(new Exception("dummy"));

            Mock<IGet<OpenWeather>> openGetter = new Mock<IGet<OpenWeather>>();
            openGetter.Setup(x => x.Get(It.IsAny<string>())).Returns(weather);

            // Arrange
            var controller = new WeatherController(getter.Object, openGetter.Object);

            // Act
            var result = controller.Get("Country", "City");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Time);
            Assert.AreEqual("Longitude 139.2, Latitude 35.2", result.Location);
            Assert.AreEqual("", result.DewPoint);
            Assert.AreEqual("1013", result.Pressure);
            Assert.AreEqual("89", result.RelativeHumidity);
            Assert.AreEqual("overcast clouds", result.SkyConditions);
            Assert.AreEqual("289.5 degrees C", result.Temperature);
            Assert.AreEqual("", result.Visibility);
            Assert.AreEqual("Strength 7.31 from 187.002 degrees", result.Wind);
        }
    }
}