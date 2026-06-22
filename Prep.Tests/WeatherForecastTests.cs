using Prep;

namespace Prep.Tests;

public class WeatherForecastTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    [InlineData(-40)]
    [InlineData(20)]
    public void TemperatureF_ConvertsCelsiusUsingProjectFormula(int celsius)
    {
        var forecast = new WeatherForecast { TemperatureC = celsius };
        var expected = 32 + (int)(celsius / 0.5556);

        Assert.Equal(expected, forecast.TemperatureF);
    }
}
