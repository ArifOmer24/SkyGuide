using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkyGuide.Services
{
    public class WeatherService
    {
        private const string ApiKey = "399ef5f1ecb05bee14a6130710f99f66";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/forecast";


        public async Task<List<WeatherForecast>> GetWeatherForecastAsync (string cityName)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"{BaseUrl}?q={cityName}&units=metric&appid={ApiKey}";
                var response = await client.GetStringAsync(url);
                return ParseWeatherData(response);
            }
        }

        private List<WeatherForecast> ParseWeatherData(string jsonResponse)
        {
            List<WeatherForecast> forecasts = new List<WeatherForecast>();
            var data = JObject.Parse(jsonResponse)["list"];

            foreach (var item in data.Take(5))
            {
                forecasts.Add(new WeatherForecast
                {
                    Date = DateTime.Parse(item["dt_txt"].ToString()).ToString("dd MMM yyyy"),
                    Temperature = $"{item["main"]["temp"]}°C",
                    Condition = item["weather"][0]["description"].ToString()
                });
            }
            return forecasts;
        }

    }
}
