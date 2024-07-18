using Microsoft.AspNetCore.Mvc;
using Test.Business;
using Test.Business.Model;
using Test.Business.UserService;
using Test.Entities;
namespace Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequestModel user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userService.AddUser(user);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            var dbUser = await _userService.GetUserByUsername(user.Username);
            if (dbUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var token = _jwtService.GenerateToken(dbUser);
            return Ok(new { token });
        }
    }
}
