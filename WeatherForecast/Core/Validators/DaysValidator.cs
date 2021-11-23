using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Core.Validators
{
    public class DaysValidator : AbstractValidator<int>
    {
        public DaysValidator()
        {
            RuleFor(x => x).NotNull().WithMessage("Days cannot be empty");
            RuleFor(x => x).ExclusiveBetween(1,14).WithMessage("Expected value between 1..14");
        }
    }
}
