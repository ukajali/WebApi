using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Features.ClimateFeatures
{
    public class GetClimatesHandler : IRequestHandler<GetClimates, IEnumerable<Climate>>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly Location _location;

        public GetClimatesHandler(IDatabaseContext dbContext, Location location = null)
        {
            _dbContext = dbContext;
            _location = location;
        }
        public Task<IEnumerable<Climate>> Handle(GetClimates request, CancellationToken cancellationToken)
        {
            var climateList = new List<Climate>();
            if(_location != null)
            {
                climateList.Add(_dbContext.LocationClimates.AsEnumerable().Where(n => n.Location == _location).FirstOrDefault());
                return Task.FromResult(climateList.AsEnumerable());
            }
            else
            {
                return Task.FromResult(_dbContext.LocationClimates.AsEnumerable());
            }
            
        }
    }
}
