using HelloDot.models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HelloDot.Clients
{
    public class WeatherClient
    {

        private readonly HttpClient httpClient;

        private readonly ServiceSettings settings;


        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            this.httpClient = httpClient;
            settings = options.Value; //will get the data from appsettings.json.
        }

        public record Weather(string description); //is like create a class DTO to get the data from the id, you can create a mold to the object got from the APi

        public record Main(decimal temp);
        public record Forecast(Weather[] weather, Main main, long dt);


        public async Task<Forecast> getCurrentWeatherAsync(string city)
        {
            var forecast = await httpClient.GetFromJsonAsync<Forecast>($"https://{settings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={settings.ApiKey}&units=metric");


            return forecast;


        }
    }
}
