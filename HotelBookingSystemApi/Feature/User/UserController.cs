using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystemApi.Feature.User
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController :BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly BL_User _bL_User;
        private readonly ResponseModel _response;

        public UserController(ILogger<UserController> logger,BL_User bL_User
            , IServiceProvider serviceProvider, ResponseModel response)
            : base(serviceProvider)
        {
            _logger = logger;
            _bL_User = bL_User;
            _response = response;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet(Name ="GetUser")]
        
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var userLst = await _bL_User.GetUsers();
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = userLst.DataLst.Count,
                    IsSuccess = userLst.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.User,
                    Message = userLst.MessageResponse.Message,
                    Item = userLst.DataLst
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting users");
                return InternalServerError(ex);
            }
        }

        [HttpGet("{PageSize}/{PageNo}")]
        public async Task<IActionResult> GetUsers(int PageSize, int PageNo)
        {
            try
            {
                var lst = await _bL_User.GetUsers(PageSize, PageNo);
                
                var responseModel = _response.Return
               (new ReturnModel
               {
                  // Token = RefreshToken(),
                   Count = lst.DataLst.Count,
                   EnumBooking = EnumBooking.User,
                   IsSuccess = lst.MessageResponse.IsSuccess,
                   Message = lst.MessageResponse.Message,
                   Item = lst.DataLst,
                   PageSetting = lst.PageSetting
               });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                var item = await _bL_User.GetUser(userId);
                var responseModel = _response.Return
               (new ReturnModel
               {
                   Token = RefreshToken(),
                   EnumBooking = EnumBooking.User,
                   IsSuccess = item.MessageResponse.IsSuccess,
                   Message = item.MessageResponse.Message,
                   Item = item.Data
               });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel requestModel)
        {
            try
            {
                var user = await _bL_User.CreateUser(requestModel);
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
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserModel requestModel)
        {
            try
            {
                var model = await _bL_User.UpdateUser(userId, requestModel);
                var responseModel = _response.Return
              (new ReturnModel
              {
                  Token = RefreshToken(),
                  EnumBooking = EnumBooking.User,
                  IsSuccess = model.IsSuccess,
                  Message = model.Message,
                  Item = requestModel
              });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            } ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var model = await _bL_User.DeleteUser(id);
              var responseModel = _response.Return
              (new ReturnModel
              {
                  Token = RefreshToken(),
                  EnumBooking = EnumBooking.User,
                  IsSuccess = model.IsSuccess,
                  Message = model.Message,
              });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            };
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUser(string reqData)
        {
            try
            {
                var item = await _bL_User.SearchUser(reqData);
                var responseModel = _response.Return
               (new ReturnModel
               {
                   Token = RefreshToken(),
                   EnumBooking = EnumBooking.User,
                   IsSuccess = item.MessageResponse.IsSuccess,
                   Message = item.MessageResponse.Message,
                   Item = item.DataLst
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
