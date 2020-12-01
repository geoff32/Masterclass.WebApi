using MasterClass.Service.Abstractions.Users;
using MasterClass.Service.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace MasterClass.WebApi.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpPost, Route("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateParameters authParams)
        {
            var authUser = _userService.Authenticate(authParams);
            return authUser == null ? (IActionResult)Unauthorized() : Ok(authUser);
        }
        
        [HttpGet]
        public IActionResult GetContext() => Ok(new { Id = User.Identity.Name });
    }
}