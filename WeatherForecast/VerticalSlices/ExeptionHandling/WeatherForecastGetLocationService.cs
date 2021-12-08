using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;
using WeatherForecast.VerticalSlices;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.VerticalSlices.ExeptionHandling;

namespace WeatherForecast.WebApi.ExeptionHandling
{
    public interface IWeatherForecastGetLocationService
    {
        Task<Climate> Get(string country, string city);
    }
    public class WeatherForecastGetLocationService : IWeatherForecastGetLocationService
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IValidator<Location> _locationValidator;
        public WeatherForecastGetLocationService(
            IValidator<ClimateRequest> climateRequestValidator,
            IDatabaseContext dbContext,
            IValidator<Location> locationValidator)
        {
            _dbContext = dbContext;
            _locationValidator = locationValidator;
        }
        public async Task<Climate> Get(string country, string city)
        {
            var newLocation = new Location(country, city);
            var validationResult = await _locationValidator.ValidateAsync(newLocation);
            if (!validationResult.IsValid)
                throw new LocationException(ValidationResultTranslator.ValidationResultTranslate(validationResult));

            var serviceResult = _dbContext.LocationClimates
                .FirstOrDefault(x => x.Location.City == city && x.Location.Country == country);
            return serviceResult;
        }    
    } 
}
