using FluentValidation;
using WeatherForecast.Core.Model;

namespace WeatherForecast.VerticalSlices
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
