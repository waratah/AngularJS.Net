using System.Collections.Generic;
using System.Data;
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

    public class CitiesController : ApiController
    {
        IGet<string> _getter;

        public CitiesController()
        {
            var get = new XmlGet<string>
            {
                CleanUpString = " xmlns=\"http://www.webserviceX.NET\""
            };
            _getter = get;
        }

        public CitiesController(IGet<string> getter)
        {
            _getter = getter;
        }

        // GET api/values
        public IEnumerable<string> Get(string id)
        {
            var data = _getter.Get("http://www.webservicex.net/globalweather.asmx/GetCitiesByCountry?countryName=" + id);

            var xml = XDocument.Parse(data);

            var result= xml
                .Descendants("City")
                .Select(x=>x.Value.Trim())
                .OrderBy(x => x);

            return result.ToList();
        }
    }
}
