using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using UnitTests.Fakes;
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

        private const string AnyLocation = "germany/bonn";

        public GetWeatherForecastQueryTests()
        {
            _temperatureRepositoryMock = new Mock<ITemperatureRepository>();
            _temperatureRepositoryMock
                .Setup(x => x.Get(AnyLocation))
                .Returns(new TemperatureRange(0, 10));
            
            _fakeNowProvider = new Mock<INowProvider>();

            _sut = new GetWeatherForecastQueryHandler(
                _temperatureRepositoryMock.Object, 
                _fakeNowProvider.Object, 
                new FakeRandomGenerator(0));
        }
        

        [Theory]
        [InlineData(5)]
        [InlineData(14)]
        public async Task GenerateForecast_VerifyDates(int days)
        {
            // arrange
            var testTime = new DateTime(2021, 12, 01);
            _fakeNowProvider.Setup(x => x.Now()).Returns(testTime);
            var request = new GetWeatherForecastQuery(days, AnyLocation);

            // act
            var forecasts = (await _sut.Handle(request, CancellationToken.None))
                .ToList();

            // assert
            for(var x = 0; x < days; x++)
            {
                Assert.Equal(testTime.AddDays(x+1), forecasts[x].Date);
            }          
        }
    }
}