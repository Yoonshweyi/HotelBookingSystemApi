using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.RoomCategory;
using HotelBookingSystem.Model.Setup.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HotelBookingSystemApi.Feature.RoomCategory
{
    [Route("api/v1/roomCategory")]
    [ApiController]
    public class RoomCategoryController : BaseController
    {
        private readonly BL_RoomCategory _bL_RoomCategory;
        private readonly ResponseModel _response;

        public RoomCategoryController(IServiceProvider serviceProvider,BL_RoomCategory bL_RoomCategory,
            ResponseModel response) : base(serviceProvider)
        {
            _bL_RoomCategory = bL_RoomCategory;
            _response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomCategory()
        {
            try
            {
                var roomCategoryLst = await _bL_RoomCategory.GetRoomCategory();
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = roomCategoryLst.DataLst.Count,
                    IsSuccess = roomCategoryLst.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.RoomCategory,
                    Message = roomCategoryLst.MessageResponse.Message,
                    Item = roomCategoryLst.DataLst
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetRoomCategory(int pageNo, int pageSize)
        {
            try
            {
                var roomCategoryLst = await _bL_RoomCategory.GetRoomCategories(pageNo, pageSize);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = roomCategoryLst.DataLst.Count,
                    IsSuccess = roomCategoryLst.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.RoomCategory,
                    Message = roomCategoryLst.MessageResponse.Message,
                    Item = roomCategoryLst.DataLst,
                    PageSetting = roomCategoryLst.PageSetting
                });

                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("{CategoryId}")]
        public async Task<IActionResult> GetCategoryById(int CategoryId)
        {
            try
            {
                var roomCategory = await _bL_RoomCategory.GetRoomCategoryById(CategoryId);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    IsSuccess = roomCategory.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.RoomCategory,
                    Message = roomCategory.MessageResponse.Message,
                    Item = roomCategory.Data
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoomCategory(RoomCategoryModel requestModel)
        {
            try
            {
                var roomCategory = await _bL_RoomCategory.CreateRoomCategory(requestModel);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    IsSuccess = roomCategory.IsSuccess,
                    EnumBooking = EnumBooking.User,
                    Message = roomCategory.Message,
                    Item = requestModel
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateRoomCategory(int Id, [FromBody] RoomCategoryModel requestModel)
        {
            try
            {
                var model = await _bL_RoomCategory.UpdateRoomCategory(Id, requestModel);
                var responseModel = _response.Return
              (new ReturnModel
              {
                  Token = RefreshToken(),
                  EnumBooking = EnumBooking.RoomCategory,
                  IsSuccess = model.IsSuccess,
                  Message = model.Message,
                  Item = requestModel
              });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            };
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomCategory(int id)
        {
            try
            {
                var model = await _bL_RoomCategory.DeleteRoomCategory(id);
                var responseModel = _response.Return
              (new ReturnModel
              {
                  Token = RefreshToken(),
                  EnumBooking = EnumBooking.RoomCategory,
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

    }
    }
