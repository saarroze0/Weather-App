using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weather_App.DTOs
{
    public class WeatherData
    {
        public Main Main { get; set; }
        public string Name { get; set; }
        public List<WeatherDescription> Weather { get; set; }
    }
}