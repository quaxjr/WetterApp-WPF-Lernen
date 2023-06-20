using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WetterApp.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherService _service;
        private string _temp;
        private string _weatherInfo;
        private string _backgroundImage;
        private string _cityQuery;

        public string Temp
        {
            get { return _temp; }
            set
            {
                _temp = value;
                OnPropertyChanged();
            }
        }

        public string WeatherInfo
        {
            get { return _weatherInfo; }
            set
            {
                _weatherInfo = value;
                OnPropertyChanged();
            }
        }

        public string BackgroundImage
        {
            get { return _backgroundImage; }
            set
            {
                _backgroundImage = value;
                OnPropertyChanged();
            }
        }

        public string CityQuery
        {
            get { return _cityQuery; }
            set
            {
                _cityQuery = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; private set; }

        public WeatherViewModel()
        {
            _service = new WeatherService();
            SearchCommand = new RelayCommand(Search, param => !string.IsNullOrEmpty(CityQuery));
            CityQuery = "Bielefeld";
            InitialUpdateUI("Bielefeld");
        }

        public void Search(object obj)
        {
            UpdateUI(CityQuery);
        }

        public void InitialUpdateUI(string city)
        {
            UpdateUI(city);
        }

        public void UpdateUI(string city)
        {
            WeatherMapResponse result = _service.GetWeatherData(city);
            string finalImage = "";

            if (result.weather[0].main.ToLower().Contains("cloud"))
            {
                finalImage = "cloud.png";
            }
            else if (result.weather[0].main.ToLower().Contains("rain"))
            {
                finalImage = "rain.png";
            }
            else if (result.weather[0].main.ToLower().Contains("snow"))
            {
                finalImage = "snow.png";
            }
            else
            {
                finalImage = "sun.png";
            }

            BackgroundImage = "Images/" + finalImage;
            Temp = result.main.temp.ToString("F1") + "°C";
            WeatherInfo = result.weather[0].main;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
