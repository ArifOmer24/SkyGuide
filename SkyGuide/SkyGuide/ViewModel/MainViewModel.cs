using SkyGuide.Model;
using SkyGuide.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGuide.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly WeatherService _weatherService;

        public List<City> Cities { get; set; }
        public ObservableCollection<WeatherForecast> SelectedCityWeather { get; set; }
        private City _selectedCity;


        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
                LoadWeatherForecastAsync();
            }
        }


        public MainViewModel() 
        {
            _weatherService = new WeatherService();
            Cities = new List<City>
            {
                new City {Name = "Istanbul"},
                new City {Name = "Paris"},
                new City {Name = "Basel"},
            };
            SelectedCityWeather = new ObservableCollection<WeatherForecast>();
        }
        private async void LoadWeatherForecastAsync()
        {
            SelectedCityWeather.Clear();
            if (SelectedCity != null)
            {
                var forecasts = await _weatherService.GetWeatherForecastAsync(SelectedCity.Name);
                foreach (var forecast in forecasts)
                {
                    SelectedCityWeather.Add(forecast);
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
