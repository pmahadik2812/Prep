using Microsoft.Extensions.Logging.Abstractions;
using Prep.Controllers;

namespace Prep.Tests;

public class WeatherForecastControllerTests
{
    private static readonly string[] ValidSummaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private readonly WeatherForecastController _controller = new(NullLogger<WeatherForecastController>.Instance);

    [Fact]
    public void Get_ReturnsFiveForecasts()
    {
        var result = _controller.Get();

        Assert.Equal(5, result.Count());
    }

    [Fact]
    public void Get_ReturnsForecastsWithDatesStartingTomorrow()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var result = _controller.Get().ToList();

        Assert.Equal(today.AddDays(1), result[0].Date);
        Assert.Equal(today.AddDays(5), result[4].Date);
    }

    [Fact]
    public void Get_ReturnsForecastsWithTemperatureInExpectedRange()
    {
        var result = _controller.Get();

        Assert.All(result, forecast =>
        {
            Assert.InRange(forecast.TemperatureC, -20, 54);
        });
    }

    [Fact]
    public void Get_ReturnsForecastsWithValidSummaries()
    {
        var result = _controller.Get();

        Assert.All(result, forecast =>
        {
            Assert.Contains(forecast.Summary, ValidSummaries);
        });
    }

    [Fact]
    public void Get_ReturnsForecastsWithPopulatedSummary()
    {
        var result = _controller.Get();

        Assert.All(result, forecast =>
        {
            Assert.False(string.IsNullOrWhiteSpace(forecast.Summary));
        });
    }
}
