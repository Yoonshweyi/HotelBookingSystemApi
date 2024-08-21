using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model.Setup.Booking;
using HotelBookingSystem.Model.Setup.Payment;
using HotelBookingSystem.Model.Setup.Room;
using HotelBookingSystem.Model.Setup.RoomCategory;
using HotelBookingSystem.Model.Setup.User;


namespace HotelBookingSystem.Model
{
    public static class ChangeModel
    {
        #region user
        public static UserModel Change(this TblUser dataModel)
        {
            var User = new UserModel()
            {
                UserId = dataModel.UserId,
                Name = dataModel.Name,
                PhoneNo = dataModel.PhoneNo,
                Password = dataModel.Password,
                Address = dataModel.Address,
                Gender = dataModel.Gender,
                Role = dataModel.Role
            };
            return User;
        }
        public static TblUser Change(this UserModel requestModel)
        {
            var User = new TblUser()
            {
                UserId = requestModel.UserId,
                Name = requestModel.Name,
                PhoneNo = requestModel.PhoneNo,
                Password = requestModel.Password,
                Address = requestModel.Address,
                Gender = requestModel.Gender,
                Role = requestModel.Role
            };
            return User;
        }


        #endregion
        #region roomCategory
        public static RoomCategoryModel Change(this TblRoomCategory dataModel)
        {
            var RoomCategoryModel = new RoomCategoryModel()
            {
                Id = dataModel.Id,
                RoomType = dataModel.RoomType
            };
            return RoomCategoryModel;
        }

        public static TblRoomCategory Change(this RoomCategoryModel requestModel)
        {
            var RoomCategory = new TblRoomCategory()
            {
                Id = requestModel.Id,
                RoomType = requestModel.RoomType,
            };
            return RoomCategory;
        }
        #endregion

        #region room
        public static RoomModel Change(this TblRoom dataModel)
        {
            var Room = new RoomModel()
            {
                RoomId = dataModel.RoomId,
                RoomNo = dataModel.RoomNo,
                Price = dataModel.Price,
                facilities = dataModel.facilities,
                Status = dataModel.Status,
                Image = dataModel.Image,
                RoomCategoryId = dataModel.RoomCategoryId
            };
            return Room;
        }

        public static TblRoom Change(this RoomModel requestModel)
        {
            var Room = new TblRoom()
            {
                RoomId = requestModel.RoomId,
                RoomNo = requestModel.RoomNo,
                Price = requestModel.Price,
                facilities = requestModel.facilities,
                Status = requestModel.Status,
                Image = requestModel.Image,
                RoomCategoryId = requestModel.RoomCategoryId
            };
            return Room;
        }
        #endregion room
        #region Booking
        public static BookingModel Change(this TblBooking dataModel)
        {
            var Booking = new BookingModel()
            {
                BookingId = dataModel.BookingId,
                UserId = dataModel.UserId,
                RoomId = dataModel.RoomId,
                CheckInDate = dataModel.CheckInDate,
                CheckOutDate = dataModel.CheckOutDate,
                TotalAmount = dataModel.TotalAmount,
                PaymentStatus = dataModel.PaymentStatus,
                PaymentDate = dataModel.PaymentDate,
                CreatedDate = dataModel.CreatedDate,
                UpdatedAt = dataModel.UpdatedAt
            };
            return Booking;
        }

        public static TblBooking Change(this BookingModel requestModel)
        {
            var Booking = new TblBooking()
            {
                BookingId = requestModel.BookingId,
                UserId = requestModel.UserId,
                RoomId = requestModel.RoomId,
                CheckInDate = requestModel.CheckInDate,
                CheckOutDate = requestModel.CheckOutDate,
                TotalAmount = requestModel.TotalAmount,
                PaymentStatus = requestModel.PaymentStatus,
                PaymentDate = requestModel.PaymentDate,
                CreatedDate = requestModel.CreatedDate,
                UpdatedAt = requestModel.UpdatedAt
            };
            return Booking;
        }
        #endregion
        #region Payment
        public static PaymentModel Change(this TblPayment dataModel)
        {
            var Payment = new PaymentModel()
            {
                PaymentId = dataModel.PaymentId,
                BookingId = dataModel.BookingId,
                PaymentMethod = dataModel.PaymentMethod,
                Amount = dataModel.Amount,
                PaymentDate = dataModel.PaymentDate

            };
            return Payment;
        }

        public static TblPayment Change(this PaymentModel requestModel)
        {
            var Payment = new TblPayment()
            {
                PaymentId = requestModel.PaymentId,
                BookingId = requestModel.BookingId,
                PaymentMethod = requestModel.PaymentMethod,
                Amount = requestModel.Amount,
                PaymentDate = requestModel.PaymentDate
            };
            return Payment;
        }
        #endregion


    }
}
