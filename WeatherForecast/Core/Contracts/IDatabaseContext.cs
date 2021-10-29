using System.Linq;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Contracts
{
    public interface IDatabaseContext
    {
        IQueryable<Climate> LocationClimates { get; }
        void AddClimate(Climate climate);
    }
}
