using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Validators
{
    public class ClimateRequestValidator : AbstractValidator<ClimateRequest>
    {
        public ClimateRequestValidator()
        {
            RuleFor(x => x.Location).NotEmpty().WithMessage("Location is empty");
            RuleFor(x => x.LowTemperature).NotEmpty().WithMessage("Location is empty");
            RuleFor(x => x.HighTemperature).NotEmpty().WithMessage("Location is empty");
        }     
    }
}
