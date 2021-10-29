using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Features.ClimateFeatures
{
    public class CreateClimateHandler : IRequestHandler<CreateClimate, Climate>
    {
        private readonly IDatabaseContext _dbContext;
       
        public CreateClimateHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Climate> Handle(CreateClimate request, CancellationToken cancellationToken)
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
