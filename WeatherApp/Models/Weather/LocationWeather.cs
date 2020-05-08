using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Weather
{
    public class LocationWeather
    {

        //location name
        public string name { get; set; }

        //api returns temp info
        public object main { get; set; }

        //api returns info on weather
        public object weather { get; set; }

    }
}
