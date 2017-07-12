using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

using IAssetTechnical.Interfaces;

namespace IAssetTechnical.Services
{
    public class JsonGet<T> : IGet<T> where T : class
    {
        public T Get(string url)
        {
            var json = GetData(url);
            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public IEnumerable<T> GetList(string url)
        {
            var json = GetData(url);
            List<T> result = JsonConvert.DeserializeObject<List<T>>(json);
            return result;
        }

        public virtual string GetData(string url)
        {
            using (var wc = new System.Net.WebClient())
            {
                return wc.DownloadString(url);
            }
        }
    }
}