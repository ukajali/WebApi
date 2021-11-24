using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using UnitTests.Fakes;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Features.ForecastFeatures;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;
using Xunit;

namespace UnitTests
{
    public class GetWeatherForecastQueryTests
    {
        private readonly GetForecastHandler _sut;
        private readonly Mock<INowProvider> _fakeNowProvider;
        private Location AnyLocation = new Location("germany", "bonn");

        public GetWeatherForecastQueryTests()
        {
            var temperatureRepositoryMock = new Mock<ITemperatureRepository>();
            temperatureRepositoryMock
                .Setup(x => x.Get(AnyLocation))
                .Returns(new TemperatureRange(0, 10));
            
            _fakeNowProvider = new Mock<INowProvider>();

            _sut = new GetForecastHandler(
                temperatureRepositoryMock.Object, 
                _fakeNowProvider.Object, 
                new FakeRandomGenerator(1));
        }
          
        [Fact]
        public async Task Generate3DaysForecast()
        {
            // arrange
            SetupNow(30.May(2021).At(21,13));
            var request = new GetForecast(3, AnyLocation);

            // act
            var forecast = (await _sut.Handle(request, CancellationToken.None)).ToList();

            // asset
            forecast.Should().BeEquivalentTo(new ForecastPoint[]
            {
                new() {Date = 31.May(2021).At(21,13), TemperatureC = 2, Summary = "Bracing"},
                new() {Date = 1.June(2021).At(21,13), TemperatureC = 4, Summary = "Hot"},
                new() {Date = 2.June(2021).At(21,13), TemperatureC = 6, Summary = "Mild"}
            });
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(14)]
        public async Task GenerateForecast_VerifyDates(int days)
        {
            // arrange
            var nowDateTime = 1.December(2021).At(8,58);
            SetupNow(nowDateTime);
            var request = new GetForecast(days, AnyLocation);

            // act
            var forecasts = (await _sut.Handle(request, CancellationToken.None))
                .ToList();

            // assert
            forecasts.Should().BeEquivalentTo(
                Enumerable.Range(1,days).Select(idx => new { Date = nowDateTime.AddDays(idx) }),
                options => options.WithStrictOrdering());
        }

        private void SetupNow(DateTime dateTime) => _fakeNowProvider
            .Setup(x => x.Now())
            .Returns(dateTime);
    }
}