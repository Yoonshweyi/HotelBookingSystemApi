using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.Booking;
using HotelBookingSystem.Model.Setup.Room;
using HotelBookingSystemApi.Feature.Room;

namespace HotelBookingSystemApi.Feature.Booking
{
    public class BL_Booking
    {
        private readonly DL_Booking _dL_booking;
        public BL_Booking(DL_Booking dL_Booking) => _dL_booking = dL_Booking;

        #region Get Booking
        public async Task<BookingListResponseModel> GetBooking()
        {
              var response=await _dL_booking.GetBooking();
            return response;
        }
        #endregion

        #region  Get Booking With Pagination
        public async Task<BookingListResponseModel> GetBooking(int PageSize, int PageNo)
        {
            if (PageSize <= 0) throw new Exception("PageSize is not less than 0.");
            if (PageNo <= 0) throw new Exception("PageNo is not less than 0.");
            var model = await _dL_booking.GetBooking(PageSize, PageNo);
            return model;
        }
        #endregion

        #region Get BookingById
        public async Task<BookingResponseModel> GetBookingById(int Id)
        {
            if (Id == null) throw new Exception("BookingId is null");
            var response=await _dL_booking.GetBookingById(Id);
            return response;
        }
        #endregion

        #region Create Booking
        public async Task<MessageResponseModel> CreateBooking(BookingModel requestModel)
        {
             CheckBookingNullValue(requestModel);
            var response = await _dL_booking.CreateBooking(requestModel);
            return response;
        }
        #endregion

        #region Update Booking
        public async Task<MessageResponseModel> UpdateBooking(int id, BookingModel requestModel)
        {
            if (id == 0) throw new Exception("BookingId is 0.");
             CheckBookingNullValue(requestModel);
            var model = await _dL_booking.UpdateBooking(id, requestModel);
            return model;
        }
        #endregion

        #region Delete Booking
        public async Task<MessageResponseModel> DeleteBooking(int id)
        {
            if (id == 0) throw new Exception("id is 0.");
            var model = await _dL_booking.DeleteBooking(id);
            return model;
        }
        #endregion

        private void CheckBookingNullValue(BookingModel requestModel)
        {
            if (requestModel == null)
            {
                throw new Exception("BookingModel is null.");
            }

            if (requestModel.CheckInDate ==null)
            {
                throw new Exception("CheckInDate is null.");
            }
            if (requestModel.CheckOutDate == null)
            {
                throw new Exception("CheckOutDate is null");
            }
            if (requestModel.TotalAmount==0)
            {
                throw new Exception("Total Amount is null.");
            }
            if (string.IsNullOrWhiteSpace(requestModel.PaymentStatus))
            {
                throw new Exception("Payment Status is null.");
            }
            if (requestModel.PaymentDate ==null)
            {
                throw new Exception("Payment Date is null.");
            }
        }

    }
}
