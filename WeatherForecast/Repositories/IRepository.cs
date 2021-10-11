using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(string str);
        //void Update(T obj);
        //void Add(T obj);
        //void Delete(T obj);
    }
}
