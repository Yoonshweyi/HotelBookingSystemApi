using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model.Setup.Room;
using HotelBookingSystem.Model;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.Model.Setup.Booking;
using HotelBookingSystem.Model.Setup.PageSetting;
using HotelBookingSystem.Shared;

namespace HotelBookingSystemApi.Feature.Booking
{
    public class DL_Booking
    {
        private readonly AppDbContext _appDbContext;
        public DL_Booking(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        #region Get Booking
        public async Task<BookingListResponseModel> GetBooking()
        {
            var responseModel = new BookingListResponseModel();
            try
            {
                var booking = await _appDbContext
                    .TblBookings
                    .AsNoTracking()
                    .ToListAsync();
                responseModel.DataLst = booking
                    .Select(x => x.Change())
                    .ToList();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.DataLst = new List<BookingModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region Get Booking With Pagination
        public async Task<BookingListResponseModel> GetBooking(int PageSize, int PageNo)
        {
            var responseModel = new BookingListResponseModel();
            try
            {
                var bookingList = _appDbContext.TblBookings.AsNoTracking();


                var booking = await bookingList
                    .Pagination(PageNo, PageSize)
                    .ToListAsync();

                var totalCount = await bookingList.CountAsync();
                var pageCount = totalCount / PageSize;
                if (totalCount % PageSize > 0)
                    pageCount++;

                responseModel.DataLst = booking.Select(x => x.Change()).ToList();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
                responseModel.PageSetting = new PageSettingModel(PageNo, PageSize, pageCount, totalCount);
            }
            catch (Exception ex)
            {
                responseModel.DataLst = new List<BookingModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region GetBookingById
        public async Task<BookingResponseModel> GetBookingById(int Id)
        {
            var responseModel = new BookingResponseModel();
            try
            {
                var booking = await _appDbContext
                    .TblBookings
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.BookingId == Id);
                if (booking is null)
                {
                    responseModel.MessageResponse = new MessageResponseModel
                        (false, EnumStatus.NotFound.ToString());
                    goto result;
                }

                responseModel.Data = booking.Change();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.Data = new BookingModel();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

        result:
            return responseModel;
        }
        #endregion


        #region Booking Create
        public async Task<MessageResponseModel> CreateBooking(BookingModel requestModel)
        {
            var responseModel = new MessageResponseModel();
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                var isRoomBooked = await _appDbContext.TblBookings
                 .AnyAsync(b => b.RoomId == requestModel.RoomId &&
                         b.CheckInDate < requestModel.CheckOutDate && // Existing booking's CheckInDate is before the new CheckOutDate
                         b.CheckOutDate > requestModel.CheckInDate);  // Existing booking's CheckOutDate is after the new CheckInDate

                if (isRoomBooked)
                {
                    await transaction.RollbackAsync();
                    return new MessageResponseModel(false, "This room is already booked for the selected dates.");
                }

                await _appDbContext.TblBookings.AddAsync(requestModel.Change());
                var result = await _appDbContext.SaveChangesAsync();
                var room = await _appDbContext.TblRooms.FindAsync(requestModel.RoomId);

                if (room != null)
                {
                    room.Status = "Occupied";
                    _appDbContext.TblRooms.Update(room);
                    await _appDbContext.SaveChangesAsync();
                }
                await transaction.CommitAsync();
               
                responseModel = result > 0
                    ? new MessageResponseModel(true, EnumStatus.Success.ToString())
                    : new MessageResponseModel(false, EnumStatus.Fail.ToString());
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                responseModel = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region Booking Update
        public async Task<MessageResponseModel> UpdateBooking(int id, BookingModel requestModel)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                var booking = await _appDbContext.TblBookings.FirstOrDefaultAsync(x => x.BookingId == id);
                if (booking is null)
                {
                    responseModel = new MessageResponseModel(false, EnumStatus.NotFound.ToString());
                    return responseModel;
                }

                #region Patch Method Validation Conditions

                if (requestModel.UserId != 0)
                {
                   booking.UserId = requestModel.UserId;
                }
                if (requestModel.RoomId != 0) // or another condition that signifies a valid price
                {
                    booking.RoomId = requestModel.RoomId;
                }
                if (requestModel.CheckInDate != null)
                {
                    booking.CheckInDate = requestModel.CheckInDate.Date;
                }
                if (requestModel.CheckOutDate != null) 
                {
                    booking.CheckOutDate = requestModel.CheckOutDate.Date;
                }

                if(requestModel.TotalAmount != 0)
                {
                    booking.TotalAmount = requestModel.TotalAmount;
                }

                if (!string.IsNullOrEmpty(requestModel.PaymentStatus))
                {
                    booking.PaymentStatus = requestModel.PaymentStatus;
                }
                if (requestModel.PaymentDate != null)
                {
                    booking.PaymentDate = requestModel.PaymentDate;
                }
                if(requestModel.CreatedDate != null)
                {
                    booking.CreatedDate = requestModel.CreatedDate;
                }
                if (requestModel.UpdatedAt != null) 
                {
                    booking.UpdatedAt = requestModel.UpdatedAt;
                }
                #endregion

                _appDbContext.Entry(booking).State = EntityState.Modified;
                var result = await _appDbContext.SaveChangesAsync();

                responseModel = result > 0
                    ? new MessageResponseModel(true, EnumStatus.Success.ToString())
                    : new MessageResponseModel(false, EnumStatus.Fail.ToString());
            }
            catch (Exception ex)
            {
                responseModel = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region Booking Delete
        public async Task<MessageResponseModel> DeleteBooking(int id)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                var booking = await _appDbContext
                    .TblBookings
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.BookingId == id);
                if (booking is null)
                {
                    responseModel = new MessageResponseModel(false, EnumStatus.NotFound.ToString());
                    goto result;
                }

                _appDbContext.TblBookings.Remove(booking);
                _appDbContext.Entry(booking).State = EntityState.Deleted;
                var result = await _appDbContext.SaveChangesAsync();
                responseModel = result > 0
                    ? new MessageResponseModel(true, EnumStatus.Success.ToString())
                    : new MessageResponseModel(false, EnumStatus.Fail.ToString());
            }
            catch (Exception ex)
            {
                responseModel = new MessageResponseModel(false, ex);
            }

        result:
            return responseModel;
        }
        #endregion

    }
}

