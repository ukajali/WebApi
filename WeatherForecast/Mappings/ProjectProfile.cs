using AutoMapper;
using WeatherForecast.Dto;
using WeatherForecast.Model;

namespace WeatherForecast.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
           CreateMap<ForecastPoint, WeatherForecastDto>();
        }
    }
}
