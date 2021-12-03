using FluentValidation;
using System.Linq;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Features.ForecastFeatures;

namespace WeatherForecast.Core.Validators
{
    public class GetForecastValidator : AbstractValidator<GetForecast>
    {
        public GetForecastValidator(IDatabaseContext databaseContext)
        {
            RuleFor(x => x.Days).ExclusiveBetween(1, 14);
            RuleFor(x => x.Location).NotNull();
            RuleFor(x => x.Location)
                .Matches(@"\w+\/\w+")
                .WithMessage("{PropertyName} has incorrect format, expected: country/city");
            RuleFor(x => x.Location)
                .Must(location => 
                    databaseContext.LocationClimates.Any(x=> x.Location.ToString() == location))
                .WithMessage("{PropertyName} has incorrect value {PropertyValue} which doesn't exist in the system");
        }
    }
}
