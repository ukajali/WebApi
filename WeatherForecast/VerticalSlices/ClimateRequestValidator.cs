using System.Linq;
using FluentValidation;
using WeatherForecast.Core.Contracts;

namespace WeatherForecast.VerticalSlices
{
    public class ClimateRequestValidator : AbstractValidator<ClimateRequest>
    {
        public ClimateRequestValidator(IDatabaseContext databaseContext)
        {
            var locationsInTheSystem = databaseContext.LocationClimates
                .Select(x => x.Location.ToString())
                .ToList();

            RuleFor(x => x.Location)
                .NotEmpty();
            RuleFor(x => x.Location)
                .Must(x => locationsInTheSystem.Contains(x.ToString()))
                .WithMessage("'County/City' in incorrect. Location doesn't exist in the system");
            RuleFor(x => x.LowTemperature)
                .NotEmpty()
                .InclusiveBetween(-100,100);
            RuleFor(x => x.HighTemperature)
                .NotEmpty()
                .InclusiveBetween(-100,100)
                .GreaterThan(x => x.LowTemperature);
        }     
    }
}
