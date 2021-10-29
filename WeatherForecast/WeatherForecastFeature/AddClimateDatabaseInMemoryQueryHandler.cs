using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Contracts;
using WeatherForecast.Model;
using WeatherForecast.Repositories;

namespace WeatherForecast.WeatherForecastFeature
{
    public class AddClimateDatabaseInMemoryQueryHandler : IRequestHandler<AddClimateDatabaseInMemoryQuery, Climate>
    {
        private readonly IDatabaseContext _dbContext;
       
        public AddClimateDatabaseInMemoryQueryHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Climate> Handle(AddClimateDatabaseInMemoryQuery request, CancellationToken cancellationToken)
        {
            var climate = new Climate { 
                Location = request.Location, 
                LowTemperature = request.LowTemperature, 
                HighTemperature = request.HighTemperature
            };

            _dbContext.AddClimate(climate);

            return Task.FromResult(climate);
        }
    }
}
