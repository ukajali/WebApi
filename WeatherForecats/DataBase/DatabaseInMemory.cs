using WeatherForecats.Dto;

namespace WeatherForecats.DataBase
{
    public class DatabaseInMemory : IDatabaseInMemory
    {
        public ClimateDto[] LocationClimates
        {
            get
            {
                return _locationClimates;
            }
        }

        private ClimateDto[] _locationClimates = new[]
        {
            new ClimateDto{ Location = "poland/cracow", LowTemperature=-15, HighTemperature=38 },
            new ClimateDto{ Location = "india/chennai", LowTemperature=-1, HighTemperature=55 },
            new ClimateDto{ Location = "usa/richfield", LowTemperature=-10, HighTemperature=42 },
            new ClimateDto{ Location = "usa/cleveland", LowTemperature=-10, HighTemperature=42 },
            new ClimateDto{ Location = "usa/newyork", LowTemperature=-10, HighTemperature=42 },
            new ClimateDto{ Location = "usa/sanfranciso", LowTemperature=-1, HighTemperature=49 },
            new ClimateDto{ Location = "usa/redmond", LowTemperature=-12, HighTemperature=38 }
        };
    }
}
