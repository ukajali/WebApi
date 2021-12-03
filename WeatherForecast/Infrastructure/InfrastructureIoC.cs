using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Features.ForecastFeatures;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;
using WeatherForecast.Core.Validators;
using WeatherForecast.Infrastructure.Providers;
using WeatherForecast.Infrastructure.Repositories;
using WeatherForecast.Infrastructure.Repositories.DataBaseInMemory;
using WeatherForecast.VerticalSlices;

namespace WeatherForecast.Infrastructure
{
    public static class InfrastructureIoC
    {
        public static void AddInfrastructureWeatherForecastServices(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseContext, MemoryDatabaseContext>();
            services.AddScoped<ITemperatureRepository, TemperatureRepository>();
            services.AddScoped<INowProvider, NowProvider>();
            services.AddScoped<IRandomGenerator, RandomGenerator>();
            services.AddScoped<IValidator<Location>, LocationValidator>();
            services.AddScoped<IValidator<Climate>, ClimateValidator>();
            services.AddScoped<IValidator<ClimateRequest>, ClimateRequestValidator>();
            services.AddScoped<IValidator<GetForecast>, GetForecastValidator>();
        }
    }
}