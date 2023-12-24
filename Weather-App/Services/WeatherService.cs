using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Weather_App.Models;
using Weather_App.DTOs;
using Newtonsoft.Json;
using System.Configuration;


namespace Weather_App.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = ConfigurationManager.AppSettings["OpenWeatherMapApiKey"];
        }
        public async Task<Weather> GetWeatherAsync(string city)
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                return MapToWeatherModel(weatherData);
            }
            else
            {
                // Handle error or return null/empty Weather object
                return new Weather();
            }
        }
        private Weather MapToWeatherModel(WeatherData weatherData)
        {
            return new Weather
            {
                City = weatherData.Name,
                Temperature = weatherData.Main.Temp,
                Description = weatherData.Weather[0].Description,
                // Add more mappings as required
            };
        }

    }
}