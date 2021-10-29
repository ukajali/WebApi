using System.Collections.Generic;
using MediatR;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Features.ClimateFeatures
{
    public class GetClimates : IRequest<IEnumerable<Climate>> { }
}