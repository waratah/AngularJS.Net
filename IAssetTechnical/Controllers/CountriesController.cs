using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IAssetTechnical.Interfaces;
using IAssetTechnical.Services;

namespace IAssetTechnical.Controllers
{
    public class CountriesController : ApiController
    {
        IGet<Dictionary<string, string>> _countries;

        public CountriesController()
        {
            _countries = new JsonGet<Dictionary<string, string>>();
        }

        public CountriesController(IGet<Dictionary<string, string>> countries)
        {
            _countries = countries;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            var data = _countries.Get("http://country.io/names.json");
            return data.Values.OrderBy(x => x);
        }
    }
}
