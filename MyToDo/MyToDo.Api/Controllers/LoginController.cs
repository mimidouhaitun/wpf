using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Dtos;
using MyToDo.Api.Parameters;
using MyToDo.Api.Service;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService) 
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<ApiResponse> Login([FromBody]UserDto userDto)
        {
           return await loginService.LoginAsync(userDto.Account, userDto.PassWord);
        }
        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] UserDto userDto)
        {
            return await loginService.RegisgerAsync(userDto);
        }
     
    }
}
