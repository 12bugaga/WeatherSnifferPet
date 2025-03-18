using DTOs.Web.WebResponse;
using Entity.MongoDb.Weather;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Weather;
using System.Net;

namespace WebWeather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet]
        [Route("WeatherForCoord")]
        public async Task<ActionResult<ResponseShell<WeatherEntity>>> Get([FromQueryAttribute] double lat, [FromQueryAttribute] double lon)
        {
            var res = await _weatherService.GetWeather(lat, lon);

            return Ok(new ResponseShell<WeatherEntity>(res));
        }

        [HttpGet]
        [Route("Weathers")]
        public async Task<ActionResult<ResponseShell<List<WeatherEntity>>>> GetAllRecords()
        {
            var res = await _weatherService.GetWeathers();

            return Ok(new ResponseShell<List<WeatherEntity>>(res));
        }
    }
}
