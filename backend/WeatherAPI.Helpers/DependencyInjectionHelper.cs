using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using WeatherAPI.DataAccess;
using WeatherAPI.DataAccess.Interfaces;
using WeatherAPI.Domain.Models;
using WeatherAPI.DataAccess.Implementations;
using WeatherAPI.Services.Interfaces;
using WeatherAPI.Services.Implementations;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;

namespace WeatherAPI.Helpers
{
    public class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
            services.AddDbContextPool<WeatherAPIDbContext>(options => options.UseMySql(connectionString, serverVersion));
        }

        public static void InjectRepository(IServiceCollection services)
        {
            services.AddTransient<IUserRepository<User>, UserRepository>();
        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IUserService<User>, UserService>();
            services.AddTransient<IWeatherService, WeatherService>();
        }
    }
}
