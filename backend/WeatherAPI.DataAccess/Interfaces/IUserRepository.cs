using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Domain.Models;

namespace WeatherAPI.DataAccess.Interfaces
{
    public interface IUserRepository<T> : IRepository<T> where T : User
    {
        User GetUserByUsername(string username);
        User LoginUser(string username, string password);
    }
}
