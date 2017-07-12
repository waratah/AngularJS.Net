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
using IAssetTechnical.Interfaces;

namespace IAssetTechnical.Tests.Controllers
{
    [TestClass]
    public class CountriesControllerTest
    {
        [TestMethod]
        public void GetCountries()
        {
            var dict = new Dictionary<string, string>
            {
                {"one", "value1"},
                {"two", "value2"},
            };

            Mock<IGet<Dictionary<string, string>>> getter = new Mock<IGet<Dictionary<string, string>>>();
            getter.Setup(x => x.Get(It.IsAny<string>())).Returns(dict);

            // Arrange
            var controller = new CountriesController(getter.Object);

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }
    }
}
