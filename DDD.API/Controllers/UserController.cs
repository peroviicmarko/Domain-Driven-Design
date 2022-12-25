using DDD.API.Exceptions;
using DDD.API.Helpers;
using DDD.API.Providers;
using DDD.Application.Common;
using DDD.Application.DTOs.User;
using DDD.Application.DTOs.User.JWT;
using DDD.Application.DTOs.User.Request;
using DDD.Application.DTOs.User.Response;
using DDD.Application.Interfaces;
using DDD.IoC.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDD.API.Controllers
{
    [Route("/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("create-account")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccount request)
        {
            bool isUsernameTaken = await _userService.IsUsernameTakenAsync(request.Username);

            if (isUsernameTaken)
            {
                throw new APIException(Constants.UsernameIsTaken, StatusCodes.Status400BadRequest);
            }

            bool isEmailTaken = await _userService.IsEmailTakenAsync(request.Email);

            if (isEmailTaken)
            {
                throw new APIException(Constants.EmailIsTaken, StatusCodes.Status400BadRequest);
            }

            if (!DataValidation.ValidateEmail(request.Email))
            {
                throw new APIException(Constants.InvalidEmail, StatusCodes.Status400BadRequest);
            }

            UserDto userDto = ObjectMapper.CreateAccountMap(request);

            var user = await _userService.CreateUserAsync(userDto);
            await _userService.SaveChangesAsync();

            if (user != null)
            {
                TokenBuilder.SignInKey = _configuration["AppSettings:SecretKey"];

                string accessToken = TokenBuilder.GenerateAccessToken(user);
                string refreshToken = TokenBuilder.GenerateRefreshTokenToken(user);

                Tokens tokens = new (accessToken, refreshToken);

                AccountCreatedResponse response = new(user, tokens);

                string successMessage = Constants.AccountCreated.Replace("{{FULLNAME}}", user.Fullname).Replace("{{USERNAME}}", user.Username);
                Logger.Custom(successMessage, "auth");

                return Ok(response);
            }

            throw new APIException(Constants.FailedToCreateAccount, StatusCodes.Status500InternalServerError);
        }
    }
}
