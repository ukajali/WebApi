using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Model;

namespace WeatherForecast.WeatherForecastFeature
{
    public class GetAllLocationsQuery : IRequest<IEnumerable<Climate>>
    {
    
    }
}
