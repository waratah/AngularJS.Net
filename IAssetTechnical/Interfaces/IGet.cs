using System.Collections.Generic;

namespace IAssetTechnical.Interfaces
{
    public interface IGet<T> where T : class
    {
        T Get(string url);
        IEnumerable<T> GetList(string url);
    }
}
