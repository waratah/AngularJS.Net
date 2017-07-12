using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using IAssetTechnical;
using IAssetTechnical.Services;
using IAssetTechnical.Models;

namespace IAssetTechnical.Tests
{

    [TestClass]
    public class GetJsonTest
    {

        /// <summary>
        /// There are too many asserts at the bottom, would normally break this up into a few more tests.
        /// </summary>
        [TestMethod]
        public void GetJsonOpenWeather()
        {
            var json = @"{'coord':{'lon':139.2,'lat':-35.2},
'sys':{'country':'JP','sunrise':1369769524,'sunset':1369821049},
'weather':[{'id':804,'main':'clouds','description':'overcast clouds','icon':'04n'}],
'main':{'temp':289.5,'humidity':89,'pressure':1013,'temp_min':287.04,'temp_max':292.04},
'wind':{'speed':7.31,'deg':187.002},
'rain':{'3h':0},
'clouds':{'all':92},
'dt':1369824698,
'id':1851632,
'name':'Shuzenji',
'cod':200}";

            var mock = new Mock<JsonGet<OpenWeather>>();
            mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(json);

            var sut = mock.Object;
            // Act
            var result = sut.Get("dummy");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(139.2M, result.Coord.Lon);
            Assert.AreEqual(-35.2M, result.Coord.Lat);

            Assert.AreEqual("JP", result.Sys.Country);
            Assert.AreEqual(1369769524, result.Sys.Sunrise);
            Assert.AreEqual(1369821049, result.Sys.Sunset);

            var weather = result.Weather.First();
            Assert.AreEqual(804, weather.Id);
            Assert.AreEqual("clouds", weather.Main);
            Assert.AreEqual("overcast clouds", weather.Description);

            Assert.AreEqual(289.5M, result.Main.Temp);
            Assert.AreEqual(292.04M, result.Main.Temp_max);
            Assert.AreEqual(287.04M, result.Main.Temp_min);

            Assert.AreEqual(7.31M, result.Wind.Speed);
            Assert.AreEqual(187.002M, result.Wind.Deg);

            Assert.AreEqual(92, result.Clouds.All);

            Assert.AreEqual(1369824698, result.Dt);
            Assert.AreEqual("Shuzenji", result.Name);
        }
    }
}
