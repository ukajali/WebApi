using System.Threading.Tasks;
using FluentValidation;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;

namespace WeatherForecast.VerticalSlices.ExceptionHandling
{
    public interface IWeatherForecastCreateLocationService
    {
        Task<Climate> PostLocation(ClimateRequest climateRequest);
    }
    public class WeatherForecastCreateLocationService : IWeatherForecastCreateLocationService
    { 
        private readonly IDatabaseContext _dbContext;
        private readonly IValidator<ClimateRequest> _climateRequestValidator;

        public WeatherForecastCreateLocationService(
            IDatabaseContext dbContext,
            IValidator<ClimateRequest> climateRequestValidator)
        {
            _dbContext = dbContext;
            _climateRequestValidator = climateRequestValidator;
        }
        public async Task<Climate> PostLocation(ClimateRequest climateRequest)
        {
            var validationResult = await _climateRequestValidator.ValidateAsync(climateRequest);
            if (!validationResult.IsValid)
                throw new ClimateException(ValidationResultTranslator.ValidationResultTranslate(validationResult));

            var location = new Location(climateRequest.Location);
            var climate = new Climate
            {
                Location = location,
                LowTemperature = climateRequest.LowTemperature,
                HighTemperature = climateRequest.HighTemperature
            };
            _dbContext.AddClimate(climate);
            return climate;
        }
    }
}
