using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Infrastructure.Repositories
{
    public class TemperatureRepository: ITemperatureRepository
    {
        private readonly IDatabaseContext _dbContext;
        public TemperatureRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TemperatureRange Get(string location)
        {
            var climates =
                from climate in _dbContext.LocationClimates
                where climate.Location == location
                select new TemperatureRange(climate.LowTemperature, climate.HighTemperature);
            // simulate real database response delay and async processing
            Task.Delay(2, CancellationToken.None);
            return climates.FirstOrDefault();
        }
    }
}
