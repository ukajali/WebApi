using AutoMapper;
using WeatherForecast.Dto;

namespace WeatherForecast.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
           CreateMap<Model.WeatherForecast, WeatherForecastDto>();
        }
    }
}
