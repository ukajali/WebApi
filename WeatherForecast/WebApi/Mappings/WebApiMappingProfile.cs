using AutoMapper;
using WeatherForecast.Core.Model;
using WeatherForecast.WebApi.Responses;

namespace WeatherForecast.WebApi.Mappings
{
    public class WebApiMappingProfile : Profile
    {
        public WebApiMappingProfile()
        {
           CreateMap<ForecastPoint, WeatherForecastResponse>();
        }
    }
}
