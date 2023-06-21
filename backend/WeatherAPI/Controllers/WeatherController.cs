using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using WeatherAPI.Models.Users;
using WeatherAPI.Services.Implementations;
using WeatherAPI.Services.Interfaces;
using WeatherAPI.Shared.Exceptions;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {

        private IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("current-weather")]
        //[Authorize] - Figure out how to provide the token in order for this to work
        async public Task<IActionResult> GetWeather()
        {
            string result = await _weatherService.GetCurrentWeather();
            
            if(result == null)
            {
                throw new NotFoundException("The requested resource was not found");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            
        }
    }
}
