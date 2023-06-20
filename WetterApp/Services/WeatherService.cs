using System.Net.Http;
using Newtonsoft.Json;

namespace WetterApp
{
    public class WeatherService
    {
        private readonly string apiKey = "082c0d06113cb1244d2125873e22a7fa";
        private readonly string requestUrl = "https://api.openweathermap.org/data/2.5/weather";

        public WeatherMapResponse GetWeatherData(string city)
        {
            HttpClient client = new HttpClient();
            var finalUri = requestUrl + "?q=" + city + "&appid=" + apiKey + "&units=metric";
            HttpResponseMessage httpResponse = client.GetAsync(finalUri).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(response);
            return weatherMapResponse;
        }
    }
}
