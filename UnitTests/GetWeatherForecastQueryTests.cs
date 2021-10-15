using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using WeatherForecast.Model;
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
        public async Task WhenDays5_And_LocationBonn()
        {
            // arrange
            const string location = "germany/bonn";
            const int days = 5;
            _temperatureRepositoryMock
                .Setup(x => x.Get(location))
                .Returns(new TemperatureRange(0, 10));
            
            // act
            var forecasts = await _sut.Handle(new GetWeatherForecastQuery(days, location), CancellationToken.None);

            // assert
            Assert.Equal(DateTime.Now.AddDays(1), forecasts.ToArray()[0].Date);
        }
    }
}