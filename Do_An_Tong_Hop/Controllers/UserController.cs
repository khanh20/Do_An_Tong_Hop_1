using API.Dtos.Users;
using API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger) : base(logger)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public IActionResult Create(CreateUserDto input)
        {
            try
            {
                _userService.Create(input);
                return Ok(new { message = "Tạo tài khoản thành công" });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto input)
        {
            try
            {
                string token = _userService.Login(input);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }
    }
}
