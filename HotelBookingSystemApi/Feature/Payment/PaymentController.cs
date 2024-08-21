using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.Payment;
using HotelBookingSystem.Model.Setup.Room;
using HotelBookingSystemApi.Feature.Room;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystemApi.Feature.Payment
{
    [Route("api/v1/payment")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly BL_Payment _bL_Payment;
        private readonly ResponseModel _response;

        public PaymentController(IServiceProvider serviceProvider, BL_Payment bL_Payment,
            ResponseModel response) : base(serviceProvider)
        {
            _bL_Payment = bL_Payment;
            _response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayment()
        {
            try
            {
                var paymentLst = await _bL_Payment.GetPayment();
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = paymentLst.DataLst.Count,
                    IsSuccess = paymentLst.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Payment,
                    Message = paymentLst.MessageResponse.Message,
                    Item = paymentLst.DataLst
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetPayment(int pageNo, int pageSize)
        {
            try
            {
                var paymentLst = await _bL_Payment.GetPayment(pageNo, pageSize);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = paymentLst.DataLst.Count,
                    IsSuccess = paymentLst.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Payment,
                    Message = paymentLst.MessageResponse.Message,
                    Item = paymentLst.DataLst,
                    PageSetting = paymentLst.PageSetting
                });

                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentModel requestModel)
        {
            try
            {
                var payment = await _bL_Payment.CreatePayment(requestModel);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    IsSuccess = payment.IsSuccess,
                    EnumBooking = EnumBooking.Payment,
                    Message = payment.Message,
                    Item = requestModel
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            try
            {
                var payment = await _bL_Payment.GetPaymentById(paymentId);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    IsSuccess = payment.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Payment,
                    Message = payment.MessageResponse.Message,
                    Item = payment.Data
                });
                return Content(responseModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdatePayment(int Id, [FromBody] PaymentModel requestModel)
        {
            try
            {
                var model = await _bL_Payment.UpdatePayment(Id, requestModel);
                var responseModel = _response.Return
              (new ReturnModel
              {
                  Token = RefreshToken(),
                  EnumBooking = EnumBooking.Payment,
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

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            try
            {
                var payment = await _bL_Payment.GetPaymentByBookingId(bookingId);
                var responseModel = _response.Return
                (new ReturnModel
                {
                    Token = RefreshToken(),
                    IsSuccess = payment.MessageResponse.IsSuccess,
                    EnumBooking = EnumBooking.Payment,
                    Message = payment.MessageResponse.Message,
                    Item = payment.Data
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
