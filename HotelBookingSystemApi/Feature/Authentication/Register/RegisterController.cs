using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.User;
using HotelBookingSystemApi.Feature.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBookingSystemApi.Feature.Authentication.Register
{
    [Route("api/v1/auth/register")]
    [ApiController]
    public class RegisterController : BaseController
    {
        // private readonly BL_User _bL_User;
        private readonly DL_User _dL_User;
        private readonly ResponseModel _response;

        public RegisterController(DL_User dL_User
            , IServiceProvider serviceProvider, ResponseModel response)
            : base(serviceProvider)
        {
            _dL_User = dL_User;
            _response = response;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel requestModel)
        {
            try
            {
                // var user = await _dL_User.CreateUser(requestModel);
                var user = await _dL_User.CreateUser(requestModel);

                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    IsSuccess = user.IsSuccess,
                    EnumBooking = EnumBooking.User,
                    Message = user.Message,
                    Item = requestModel
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
