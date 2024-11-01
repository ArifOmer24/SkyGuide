using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkyGuide
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string apiKey = "399ef5f1ecb05bee14a6130710f99f66"; // Buraya API anahtarınızı ekleyin
        private const string apiUrl = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&lang=en";


        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GetWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text;
            if (!string.IsNullOrEmpty(city))
            {
                string weatherData = await GetWeatherDataAsync(city);
                if (weatherData != null)
                {
                    WeatherInfoTextBlock.Text = weatherData;
                }
                else
                {
                    WeatherInfoTextBlock.Text = "Hava durumu alınamadı.";
                }
            }
        }

        private async Task<string> GetWeatherDataAsync(string city)
        {
            string requestUrl = string.Format(apiUrl, city, apiKey);
            try
            {
                var response = await httpClient.GetStringAsync(requestUrl);
                dynamic weather = JsonConvert.DeserializeObject(response);
                string description = weather.weather[0].description;
                int temperature = (int)weather.main.temp; // Convert to integer to remove decimal part
                return $"City: {city}\nTemperature: {temperature} °C\nDescription: {description}";
            }
            catch (Exception)
            {
                return "Unable to fetch weather data.";
            }
        }
    }
}
