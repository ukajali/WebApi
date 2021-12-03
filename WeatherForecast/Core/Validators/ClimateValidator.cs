using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Validators
{
    public class ClimateValidator : AbstractValidator<Climate>
    {
        public ClimateValidator()
        {
            RuleFor(x => x.HighTemperature).LessThan(x => x.LowTemperature);
        }
    }
}
