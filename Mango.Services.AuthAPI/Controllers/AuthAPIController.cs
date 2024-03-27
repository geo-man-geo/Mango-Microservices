using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new ();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var message = await _authService.Register(registrationRequestDto);
            var isRoleAssigned = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role);
            if(!string.IsNullOrEmpty(message) && message != "REGISTRATION SUCCESS" && !isRoleAssigned)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = message;
                return BadRequest(_responseDto);
            }
            _responseDto.Message = message + ": WITH THE ROLE - " + registrationRequestDto.Role;
            return Ok(_responseDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            LoginResponseDto loginResponse = await _authService.Login(loginRequestDto);
            if(loginResponse.User.Name == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or Password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Response = loginResponse;
            _responseDto.Message = "Login Successful";
            return Ok(_responseDto);
        }
    }
}
