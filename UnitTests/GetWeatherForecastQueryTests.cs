using System.Threading;
using Moq;
using WeatherForecast.Repositories;
using WeatherForecast.WeatherForecastFeature;
using Xunit;

namespace UnitTests
{
    public class GetWeatherForecastQueryTests
    {
        private readonly GetWeatherForecastQueryHandler _sut;
        private readonly Mock<ITemperatureRepository> _temperatureRepositoryMock;

        public GetWeatherForecastQueryTests()
        {
            _temperatureRepositoryMock = new Mock<ITemperatureRepository>();
            _sut = new GetWeatherForecastQueryHandler(_temperatureRepositoryMock.Object);
        }
        [Fact]
        public void Test1()
        {
            _sut.Handle(new GetWeatherForecastQuery(5, "germany/bonn"), CancellationToken.None);
        }
    }
}