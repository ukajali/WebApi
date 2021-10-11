using WeatherForecast.Model;
using WeatherForecast.Dto;

namespace WeatherForecast.Repositories.DataBaseInMemory
{
    public static class DatabaseInMemory 
    {
        public static Climate[] LocationClimates = new[]
        {

            new Climate{ Location = "poland/cracow", LowTemperature=-15, HighTemperature=38 },
            new Climate{ Location = "india/chennai", LowTemperature=-1, HighTemperature=55 },
            new Climate{ Location = "usa/richfield", LowTemperature=-10, HighTemperature=42 },
            new Climate{ Location = "usa/cleveland", LowTemperature=-10, HighTemperature=42 },
            new Climate{ Location = "usa/newyork", LowTemperature=-10, HighTemperature=42 },
            new Climate{ Location = "usa/sanfranciso", LowTemperature=-1, HighTemperature=49 },
            new Climate{ Location = "usa/redmond", LowTemperature=-12, HighTemperature=38 }
        };
    }
}
