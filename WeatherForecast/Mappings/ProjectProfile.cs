using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Dto;
using WeatherForecast.Model;

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
