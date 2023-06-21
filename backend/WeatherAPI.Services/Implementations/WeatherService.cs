using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        static HttpClient client = new HttpClient();
        string baseURL = @"https://api.openweathermap.org/data/3.0/onecall";
        string APIKey = "627e5d846e11fe2b6af4446174581998";
        string geolocationAPIURL = @"";

       

        async Task<string> IWeatherService.GetCurrentWeather()
        {
            using HttpResponseMessage response = await client.GetAsync("https://api.openweathermap.org/data/3.0/onecall?lat=88&lon=44.3&appid=627e5d846e11fe2b6af4446174581998");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;

        }
    }
}
