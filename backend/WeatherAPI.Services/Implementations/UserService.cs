using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Mappers;
using WeatherAPI.DataAccess.Interfaces;
using WeatherAPI.Domain.Models;
using WeatherAPI.Models.Users;
using WeatherAPI.Services.Interfaces;
using WeatherAPI.Shared.CustomEntities;
using WeatherAPI.Shared.Exceptions;
using WeatherAPI.Validators;

namespace WeatherAPI.Services.Implementations
{
    public class UserService : IUserService<User>
    {
        private IUserRepository<User> _userRepository;
        private IOptions<AppSettings> _options;

        public UserService(IUserRepository<User> userRepository, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _options = options;
        }


        public string Login(LoginUserModel loginUserModel)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] sha512Data = sha512.ComputeHash(Encoding.ASCII.GetBytes(loginUserModel.Password));
            string hashedPassword = Encoding.ASCII.GetString(sha512Data);

            User userDb = _userRepository.LoginUser(loginUserModel.Username, hashedPassword);

            if(userDb == null) {
                throw new NotFoundException($"No such user. Incorrect username and/or password");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("The most secret secret key of them all"); //Figure out how to extract this from appsettings, as in Program.cs
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor { 
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),
                    }
                )
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string tokenString = jwtSecurityTokenHandler.WriteToken(token);
            return tokenString;
        }

        public void Register(RegisterUserModel registerUserModel)
        {
            UserValidator.ValidateUser(registerUserModel);

            if (!(_userRepository.GetUserByUsername(registerUserModel.Username) == null))
            {
                throw new UserException("A user with this username already exists!");
            }
            User newUser = registerUserModel.ToUser();
            _userRepository.Add(newUser);
        }

    }
}
