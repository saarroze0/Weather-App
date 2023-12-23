using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weather_App.Models
{
    public class Weather
    {
        [Required]
        public string City { get; set; }
        public double Temperature { get; set; }
        
        public string Description { get; set; }

        public double Humidity { get; set; }
        public double WindSpeed { get; set; }

        public DateTime Date { get; set; }
    }
}