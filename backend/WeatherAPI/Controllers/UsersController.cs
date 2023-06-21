using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WeatherAPI.Domain.Models;
using WeatherAPI.Models.Users;
using WeatherAPI.Services.Interfaces;
using WeatherAPI.Shared.Exceptions;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService<User> _userService;

        public UsersController(IUserService<User> userService )
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserModel registerUserModel)
        {
            try
            {
                _userService.Register(registerUserModel);
                return StatusCode(StatusCodes.Status201Created, "New user successfully registered.");
            }
            catch (UserException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> LoginUser([FromBody] LoginUserModel loginUserModel)
        {
            try
            {
                string token = _userService.Login(loginUserModel);
                return StatusCode(StatusCodes.Status200OK, token);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
