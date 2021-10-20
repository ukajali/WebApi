using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using UnitTests.FakesImplementation;
using WeatherForecast.Contracts;
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
        private readonly Mock<INowProvider> _fakeNowProvider;
        private readonly Mock<IRandomGenerator> _randomGenerator;

        public GetWeatherForecastQueryTests()
        {
            _temperatureRepositoryMock = new Mock<ITemperatureRepository>();
            _randomGenerator = new Mock<IRandomGenerator>();
            _fakeNowProvider = new Mock<INowProvider>();
            _sut = new GetWeatherForecastQueryHandler(_temperatureRepositoryMock.Object, _fakeNowProvider.Object, _randomGenerator.Object);
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
            
            _randomGenerator.Setup(x => x.GetRange(It.IsAny<int>(), It.IsAny<int>())).Returns(8);

            var testTime = new DateTime(2021, 12, 01);
            _fakeNowProvider.Setup(x => x.Now()).Returns(testTime);

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