using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherAPI.Models.Users;
using WeatherAPI.Shared.Exceptions;

namespace WeatherAPI.Validators
{
    public static class UserValidator
    {
        public static void ValidateUser(RegisterUserModel registerUserModel)
        {
            if (string.IsNullOrEmpty(registerUserModel.Username) || string.IsNullOrEmpty(registerUserModel.Password))
            {
                throw new UserException("Username and password are required fields");
            }
            if (registerUserModel.Username.Length > 30)
            {
                throw new UserException("Username can contain max 30 characters");
            }
            if (registerUserModel.FirstName.Length > 50 || registerUserModel.LastName.Length > 50)
            {
                throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
            }
            if (registerUserModel.Password != registerUserModel.ConfirmedPassword)
            {
                throw new UserException("The passwords do not match!");
            }
            if (!IsPasswordValid(registerUserModel.Password))
            {
                throw new UserException("The password is not complex enough!");
            }
        }

        public static bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }
    }
}
