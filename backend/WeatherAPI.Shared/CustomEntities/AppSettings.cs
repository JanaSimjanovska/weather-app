using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAPI.Shared.CustomEntities
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string SecretKey { get; set; }
    }
}
