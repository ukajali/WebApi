using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Validators
{
    public class LocationValidator: AbstractValidator<Location>
    {
		public LocationValidator()
		{
			RuleFor(x => x.City).NotEmpty().WithMessage("City is empty");
			RuleFor(x => x.Country).NotEmpty().WithMessage("Country is empty"); 
		}
	}
}
