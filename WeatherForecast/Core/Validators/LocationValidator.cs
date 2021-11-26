using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;

namespace WeatherForecast.Core.Validators
{
    public class LocationValidator: AbstractValidator<Location>
    {
		private readonly IDatabaseContext _databaseContext;
		public LocationValidator(IDatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
			var currecntLocations = _databaseContext.LocationClimates.Select(x=>x.Location).ToList();

			RuleFor(x => x.City).NotEmpty().WithMessage("City is empty");
			RuleFor(x => x.Country).NotEmpty().WithMessage("Country is empty");
			RuleFor(x => x).Must(location => currecntLocations.Equals(location)).WithMessage("This location doesn't exist in the system");
		}
	}
}
