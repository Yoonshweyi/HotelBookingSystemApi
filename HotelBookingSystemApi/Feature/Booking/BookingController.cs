using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.Booking;
using HotelBookingSystem.Model.Setup.Room;
using HotelBookingSystemApi.Feature.Room;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystemApi.Feature.Booking
{
    [Route("api/v1/booking")]
    [ApiController]
    public class BookingController : BaseController
    {
        private readonly BL_Booking _bL_Booking;
        private readonly ResponseModel _response;

        public BookingController(IServiceProvider serviceProvider, BL_Booking bL_Booking,
            ResponseModel response) : base(serviceProvider)
        {
            _bL_Booking = bL_Booking;
            _response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooking()
        {
            try
            {
                var bookingLst = await _bL_Booking.GetBooking();
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = bookingLst.DataLst.Count,
                    IsSuccess = bookingLst.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Room,
                    Message = bookingLst.MessageResponse.Message,
                    Item = bookingLst.DataLst
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetBooking(int pageNo, int pageSize)
        {
            try
            {
                var roomLst = await _bL_Booking.GetBooking(pageNo, pageSize);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = roomLst.DataLst.Count,
                    IsSuccess = roomLst.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Booking,
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

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            try
            {
                var booking = await _bL_Booking.GetBookingById(bookingId);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    IsSuccess = booking.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Booking,
                    Message = booking.MessageResponse.Message,
                    Item = booking.Data
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingModel requestModel)
        {
            try
            {
                var room = await _bL_Booking.CreateBooking(requestModel);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    IsSuccess = room.IsSuccess,
                    EnumBooking = EnumBooking.Booking,
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
        public async Task<IActionResult> UpdateBooking(int Id, [FromBody] BookingModel requestModel)
        {
            try
            {
                var model = await _bL_Booking.UpdateBooking(Id, requestModel);
                var responseModel = _response.Return
              (new ReturnModel
              {
                  Token = RefreshToken(),
                  EnumBooking = EnumBooking.Booking,
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
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var model = await _bL_Booking.DeleteBooking(id);
                var responseModel = _response.Return
              (new ReturnModel
              {
                  Token = RefreshToken(),
                  EnumBooking = EnumBooking.Booking,
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
