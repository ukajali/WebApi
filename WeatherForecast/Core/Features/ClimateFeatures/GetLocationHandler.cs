using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Features.ClimateFeatures
{
    public class GetLocationHandler : IRequestHandler<GetClimateForLocation, Climate>
    {
        private readonly IDatabaseContext _dbContext;

        public GetLocationHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Climate> Handle(GetClimateForLocation request, CancellationToken cancellationToken)
        {
            var loc = _dbContext.LocationClimates.Where(n => n.Location.City == request.Location.City).FirstOrDefault();
            return Task.FromResult(loc);
        }

        private Task<Climate> BadRequest(string v)
        {
            throw new NotImplementedException();
        }
    }
}
