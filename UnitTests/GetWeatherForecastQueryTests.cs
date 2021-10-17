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
        private readonly Mock<TimeProvider> _timeMock = new Mock<TimeProvider>();

        public GetWeatherForecastQueryTests()
        {
            _temperatureRepositoryMock = new Mock<ITemperatureRepository>();
            _sut = new GetWeatherForecastQueryHandler(_temperatureRepositoryMock.Object);
        }
        

        [Theory]
        [InlineData(5)]
        [InlineData(14)]
        public async Task WhenDays_And_LocationBonn(int days)
        {
            // arrange
            const string location = "germany/bonn";
            _temperatureRepositoryMock
                .Setup(x => x.Get(location))
                .Returns(new TemperatureRange(0, 10));

            var testTime = DateTime.UtcNow;
            _timeMock.Setup(tp => tp.UtcNow).Returns(testTime);
            TimeProvider.Current = _timeMock.Object;

            // act
            var forecasts = await _sut.Handle(new GetWeatherForecastQuery(days, location), CancellationToken.None);

            // assert
            for(int x = 0; x < days; x++)
            {
                Assert.Equal(testTime.AddDays(x+1), forecasts.ToArray()[x].Date);
            }          
        }
    }
}