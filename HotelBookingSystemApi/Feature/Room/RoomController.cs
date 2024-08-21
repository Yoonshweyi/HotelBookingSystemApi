using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.Room;
using HotelBookingSystem.Model.Setup.RoomCategory;
using HotelBookingSystemApi.Feature.RoomCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystemApi.Feature.Room
{
    [Route("api/v1/room")]
    [ApiController]
    public class RoomController : BaseController
    {
        private readonly BL_Room _bL_Room;
        private readonly ResponseModel _response;

        public RoomController(IServiceProvider serviceProvider, BL_Room bL_Room,
            ResponseModel response) : base(serviceProvider)
        {
            _bL_Room = bL_Room;
            _response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoom()
        {
            try
            {
                var roomLst = await _bL_Room.GetRoom();
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = roomLst.DataLst.Count,
                    IsSuccess = roomLst.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Room,
                    Message = roomLst.MessageResponse.Message,
                    Item = roomLst.DataLst
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetRoom(int pageNo, int pageSize)
        {
            try
            {
                var roomLst = await _bL_Room.GetRoom(pageNo, pageSize);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = roomLst.DataLst.Count,
                    IsSuccess = roomLst.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Room,
                    Message = roomLst.MessageResponse.Message,
                    Item = roomLst.DataLst,
                    PageSetting = roomLst.PageSetting
                });

                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomById(int roomId)
        {
            try
            {
                var roomCategory = await _bL_Room.GetRoomById(roomId);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    IsSuccess = roomCategory.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Room,
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
        public async Task<IActionResult> CreateRoom(RoomModel requestModel)
        {
            try
            {
                var room = await _bL_Room.CreateRoom(requestModel);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    IsSuccess = room.IsSuccess,
                    EnumBooking = EnumBooking.Room,
                    Message = room.Message,
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
        public async Task<IActionResult> UpdateRoom(int Id, [FromBody] RoomModel requestModel)
        {
            try
            {
                var model = await _bL_Room.UpdateRoom(Id, requestModel);
                var responseModel = _response.Return
              (new ReturnModel
              {
                  Token = RefreshToken(),
                  EnumBooking = EnumBooking.Room,
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
                var model = await _bL_Room.DeleteRoom(id);
                var responseModel = _response.Return
              (new ReturnModel
              {
                  Token = RefreshToken(),
                  EnumBooking = EnumBooking.Room,
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

        [HttpPost("Availability")]
        
        public async Task<IActionResult> CheckRoomAvailability(DateTime CheckInDate, DateTime CheckOutDate,int RoomCategoryId)
        {
            try
            {
                var roomLst = await _bL_Room.CheckRoomAvailability(CheckInDate,CheckOutDate,RoomCategoryId);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = roomLst.DataLst.Count,
                    IsSuccess = roomLst.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Room,
                    Message = roomLst.MessageResponse.Message,
                    Item = roomLst.DataLst
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
