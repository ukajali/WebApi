using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Features.ForecastFeatures;

namespace WeatherForecast.Core.Validators
{
    public class GetForecastValidator : AbstractValidator<GetForecast>
    {
        private readonly IDatabaseContext _databaseContext;
        public GetForecastValidator(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            var currecntLocations = _databaseContext.LocationClimates.Select(x => x.Location).ToList();

            RuleFor(x => x.Days).ExclusiveBetween(0, 15).WithMessage("Days must be btween 1 and 14");
            RuleFor(x => x.Location).NotNull().WithMessage("Location cannot be null");
            RuleFor(x => x.Location.City).NotEmpty().WithMessage("City cannot be null");
            RuleFor(x => x.Location.Country).NotEmpty().WithMessage("Country cannot be null");
            RuleFor(x => x).Must(location => currecntLocations.Equals(location)).WithMessage("This location doesn't exist in the system");
        }
    }
}
