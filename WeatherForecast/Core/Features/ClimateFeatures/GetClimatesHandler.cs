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

        public GetClimatesHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<IEnumerable<Climate>> Handle(GetClimates request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.LocationClimates.AsEnumerable());
        }
    }
}
