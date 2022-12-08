using ExceptionHandlingLayer.USERFRIENDLYEXCEPTIONS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomExceptionFilter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        //[HttpGet]
        //public ActionResult<IEnumerable<WeatherForecast>> Get(string cityName)
        //{
        //    try
        //    {
        //        if (cityName == "Sydney")
        //        {
        //            throw new Exception($"There is No Data For City : {cityName}");
        //        }
        //        var rng = new Random();
        //        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //        {
        //            Date = DateTime.Now.AddDays(index),
        //            TemperatureC = rng.Next(-20, 55),
        //            Summary = Summaries[rng.Next(Summaries.Length)]
        //        })
        //        .ToArray();
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("NotFoundException")]
        public IEnumerable<WeatherForecast> NotFoundException(string cityName)
        {
            if (cityName == "Sydney")
            {
                //throw new Exception($"There is No Data For City : {cityName}");
                throw new NotFoundException($"There is No Data For City : {cityName}");
                
            }
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }   
        
        [HttpGet("DuplicateException")]
        public IEnumerable<WeatherForecast> DuplicateException(string cityName)
        {
            if (cityName == "Sydney")
            {
                //throw new Exception($"There is No Data For City : {cityName}");
                //throw new NotFoundException($"There is No Data For City : {cityName}");
                throw new DuplicateRecordExceptionn($"There is No Data For City : {cityName}");
                
            }
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
