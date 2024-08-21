using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.Booking;
using HotelBookingSystem.Model.Setup.Payment;
using HotelBookingSystem.Model.Setup.Room;
using HotelBookingSystemApi.Feature.Room;

namespace HotelBookingSystemApi.Feature.Payment
{
    public class BL_Payment
    {
        private readonly DL_Payment _dL_Payment;
        public BL_Payment(DL_Payment dL_Payment) => _dL_Payment = dL_Payment;

        #region Get Payment
        public async Task<PaymentListResponseModel> GetPayment()
        {
            var response = await _dL_Payment.GetPayment();
            return response;
        }
        #endregion

        #region  Get Payment With Pagination
        public async Task<PaymentListResponseModel> GetPayment(int PageSize, int PageNo)
        {
            if (PageSize <= 0) throw new Exception("PageSize is not less than 0.");
            if (PageNo <= 0) throw new Exception("PageNo is not less than 0.");
            var model = await _dL_Payment.GetPayment(PageSize, PageNo);
            return model;
        }
        #endregion

        #region Get PaymentById
        public async Task<PaymentResponseModel> GetPaymentById(int Id)
        {
            if (Id == null) throw new Exception("PaymentId is null");
            var response = await _dL_Payment.GetPaymentById(Id);
            return response;
        }
        #endregion

        #region Create Payment
        public async Task<MessageResponseModel> CreatePayment(PaymentModel requestModel)
        {
            CheckPaymentNullValue(requestModel);
            var response=await _dL_Payment.CreatePayment(requestModel);
            return response;
        }
        #endregion

        #region Update Payment
        public async Task<MessageResponseModel> UpdatePayment(int id, PaymentModel requestModel)
        {
            if (id == 0) throw new Exception("RoomId is 0.");
            CheckPaymentNullValue(requestModel);
            var model = await _dL_Payment.UpdatePayment(id, requestModel);
            return model;
        }
        #endregion

        private void CheckPaymentNullValue(PaymentModel requestModel)
        {
            if (requestModel == null)
            {
                throw new Exception("PaymentModel is null.");
            }

            if (requestModel.BookingId == 0)
            {
                throw new Exception("BookingId is null.");
            }
           
            if (string.IsNullOrWhiteSpace(requestModel.PaymentMethod))
            {
                throw new Exception("PaymentMethod is null.");
            }
            if (requestModel.Amount == 0)
            {
                throw new Exception("Amount is null.");
            }
            if (requestModel.PaymentDate == null) 
            {
                throw new Exception("Payment Date is null.");
            }
        }

        #region Get PaymentByBookingId
        public async Task<PaymentResponseModel> GetPaymentByBookingId(int bookingId)
        {
            if (bookingId == null) throw new Exception("PaymentId is null");
            var response = await _dL_Payment.GetPaymentById(bookingId);
            return response;
        }
        #endregion

    }
}
