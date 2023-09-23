using Microsoft.AspNetCore.Mvc;
using WheaterForcast.Interfaces;
using WheaterForcast.Models;

namespace WheaterForcast.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IFrocastService _frocastService;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IFrocastService frocastService, ILogger<WeatherForecastController> logger)
        {
            _frocastService = frocastService;
            _logger = logger;
        }

        //First Task

        [HttpGet]
        public async Task<ActionResult<string>> CurrentForcast([FromQuery] string query)
        {
            var forcast =await this._frocastService.ForcastTitle(query);
            if (!string.IsNullOrEmpty(forcast))
                return Ok(forcast);

            return BadRequest($"Forcast for city {query} not found");
        }

        //Second Task return forcast for 3 days
        [HttpGet]
        public async Task<ActionResult<ForcastLogicModel>> DailyForcast([FromQuery] string query)
        {
            ForcastLogicModel forcast = await this._frocastService.GetForcastAsync(query);
            if (forcast!= null)
                return Ok(forcast);

            return BadRequest($"Forcast for city {query} not found");
        }
    }
}