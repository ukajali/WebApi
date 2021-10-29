using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Contracts;
using WeatherForecast.Providers;
using WeatherForecast.Repositories;
using WeatherForecast.Repositories.DataBaseInMemory;

namespace WeatherForecast
{
    public static class ServiceCollectionExtension
    {
        public static void AddWeatherForecastServices(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseContext, MemoryDatabaseContext>();
            services.AddScoped<ITemperatureRepository, TemperatureRepository>();
            services.AddScoped<INowProvider, NowProvider>();
            services.AddScoped<IRandomGenerator, RandomGenerator>();
        }
    }
}