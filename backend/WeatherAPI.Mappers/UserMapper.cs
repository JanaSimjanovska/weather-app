using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Domain.Models;
using WeatherAPI.Models.Users;
using System.Security.Cryptography;


namespace WeatherAPI.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserModel registerUserModel)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] sha512Data = sha512.ComputeHash(Encoding.ASCII.GetBytes(registerUserModel.Password));
            string hashedPassword = Encoding.ASCII.GetString(sha512Data);
            return new User()
            {
                FirstName = registerUserModel.FirstName,
                LastName = registerUserModel.LastName,
                Username = registerUserModel.Username,
                Password = hashedPassword
            };
        }
    }
}
