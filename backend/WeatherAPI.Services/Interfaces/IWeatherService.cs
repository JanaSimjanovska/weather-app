using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Services.Interfaces
{
    public interface IWeatherService
    {
        // Razmisli dali da bidat posebni metodi za sekoja prognoza (current, hourly, 2 days, 7 days) ili so eden request cela data da ja zememe pa na frontend
        // koga bi menuvale tip na prognoza za ist grad ne bi praele nov povik za sekoj tip, ama pa, taka ako dolgo ne se promeni search-natata lokacija kje imame stale data
        public Task<string> GetCurrentWeather();

    }
}
