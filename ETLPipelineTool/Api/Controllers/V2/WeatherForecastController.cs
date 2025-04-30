using ETLPipelineTool.Application.Dtos.V2.Responses;

namespace ETLPipelineTool.Api.Controllers.V2;

[ApiVersion(2.0)]
public class WeatherForecastController(ILogger<WeatherForecastController> logger)
    : BaseApiController
{
    private static readonly string[] Summaries =
    [
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching",
    ];

    private readonly ILogger<WeatherForecastController> _logger = logger;

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecastDataDto> Get()
    {
        _logger.LogInformation("V2");
        return
        [
            .. Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecastDataDto
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                }),
        ];
    }
}
