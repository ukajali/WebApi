using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecats.Dto;
using WeatherForecats.Model;

namespace WeatherForecats.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
           CreateMap<WeatherForecast, WeatherForecastDto>();
        }
    }
}
