using System.Linq;
using FluentValidation;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model.ValueObjects;

namespace WeatherForecast.VerticalSlices
{
    public class LocationValidator: AbstractValidator<Location>
    {
		public LocationValidator(IDatabaseContext databaseContext)
		{
            var locationsInTheSystem = databaseContext.LocationClimates
	            .Select(x => x.Location.ToString())
	            .ToList();

			RuleFor(x => x)
				.Must(x => locationsInTheSystem.Contains(x.ToString()))
				.WithMessage("'County/City' in incorrect. Location doesn't exist in the system");
		}
	}
}
