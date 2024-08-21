using DotNet8.PosBackendApi.Models.Setup.Token;
using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model.Setup.Login;
using HotelBookingSystem.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HotelBookingSystemApi.Feature.Authentication.Login
{
    [Route("api/v1/auth/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BL_Login _Login;

        public LoginController(BL_Login login) => _Login = login;

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel reqModel)
        {
            var model = await _Login.Login(reqModel);
            return Ok(model);
        }
    }
}
