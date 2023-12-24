using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Weather_App.Services;
//z.7Vx^Aq8KFY-A4
namespace Weather_App.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        public ActionResult Home()
        {
            return View();
        }

        public async Task<ActionResult> ShowWeather(string city)
        {
            var weather = await _weatherService.GetWeatherAsync(city);
            return View(weather); // Make sure you have a corresponding view
        }
    }
}