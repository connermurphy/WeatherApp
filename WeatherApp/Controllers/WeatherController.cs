using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeatherApp.Models.Weather;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherApp.Controllers
{
    [Route("api/weather")]
    public class WeatherController : Controller
    {

        private IConfiguration config { get; set; }       
        private readonly string apiKey;

        private readonly IHttpClientFactory clientFactory;

        public WeatherController(IConfiguration config, IHttpClientFactory clientFactory)
        {
            this.config = config;
            this.apiKey = config.GetSection("ApiKey").Value;
            this.clientFactory = clientFactory;
        }

        //fetch locations
        [HttpGet("{location}")]
        public async Task<object> Get(string location)
        {
            
            try
            {
    
                var httpClient = clientFactory.CreateClient();

                var httpReq = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={apiKey}&units=metric");
                var httpRes = await httpClient.GetAsync(httpReq);

                var responseData = await httpRes.Content.ReadAsStringAsync();

                LocationWeather locWeatherData = JsonSerializer.Deserialize<LocationWeather>(responseData);

                switch (Convert.ToInt32(httpRes.StatusCode))
                {
                    case 404:
                        return new Exception("404");                        
                    case 200:
                        return locWeatherData;
                    default:
                        return new Exception("400");
                }

            } catch (HttpRequestException exception)
            {
                return exception;
            }
            

        }

    }
}
