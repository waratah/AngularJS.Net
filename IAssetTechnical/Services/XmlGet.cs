using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using IAssetTechnical.Interfaces;

namespace IAssetTechnical.Services
{
    public class XmlGet<T> : IGet<T> where T : class
    {
        public string CleanUpString = null;

        public T Get(string url)
        {
            var data = GetData(url);
            if (! string.IsNullOrEmpty(CleanUpString))
            {
                return Deserialise<T>(data.Replace(CleanUpString, ""));
            }
            return Deserialise<T>(data);
        }

        public IEnumerable<T> GetList(string url)
        {
            var data = GetData(url);
            return Deserialise<List<T>>(data);
        }

        static public X Deserialise<X>(string input)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(X));
            using (TextReader reader = new StringReader(input))
            {
                X result = (X)serializer.Deserialize(reader);
                return result;
            }
        }

        protected virtual string GetData(string url)
        {
            using (var wc = new System.Net.WebClient())
            {
                return wc.DownloadString(url);
            }
        }
    }
}