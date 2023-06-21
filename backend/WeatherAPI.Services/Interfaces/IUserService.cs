using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.Users;

namespace WeatherAPI.Services.Interfaces
{
    public interface IUserService<T>
    {
        void Register(RegisterUserModel registerUserModel);
        string Login(LoginUserModel loginUserModel);
    }
}
