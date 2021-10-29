using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Contracts;
using WeatherForecast.Model;

namespace WeatherForecast.WeatherForecastFeature
{
    public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, IEnumerable<Climate>>
    {
        private readonly IDatabaseContext _dbContext;

        public GetAllLocationsQueryHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<IEnumerable<Climate>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.LocationClimates.AsEnumerable());
        }
    }
}
