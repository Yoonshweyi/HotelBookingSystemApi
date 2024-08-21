using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model.Setup.RoomCategory;
using HotelBookingSystem.Model;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.Model.Setup.Room;
using HotelBookingSystem.Model.Setup.PageSetting;
using HotelBookingSystem.Shared;

namespace HotelBookingSystemApi.Feature.Room
{
    public class DL_Room
    {
        private readonly AppDbContext _appDbContext;
        public DL_Room(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Get Room
        public async Task<RoomListResponseModel> GetRoom()
        {
            var responseModel = new RoomListResponseModel();
            try
            {
                var room = await _appDbContext
                    .TblRooms
                    .AsNoTracking()
                    .ToListAsync();
                responseModel.DataLst = room
                    .Select(x => x.Change())
                    .ToList();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.DataLst = new List<RoomModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region Get Room With Pagination
        public async Task<RoomListResponseModel> GetRoom(int PageSize, int PageNo)
        {
            var responseModel = new RoomListResponseModel();
            try
            {
                var roomList = _appDbContext.TblRooms.AsNoTracking();


                var room = await roomList
                    .Pagination(PageNo, PageSize)
                    .ToListAsync();

                var totalCount = await roomList.CountAsync();
                var pageCount = totalCount / PageSize;
                if (totalCount % PageSize > 0)
                    pageCount++;

                responseModel.DataLst = room.Select(x => x.Change()).ToList();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
                responseModel.PageSetting = new PageSettingModel(PageNo, PageSize, pageCount, totalCount);
            }
            catch (Exception ex)
            {
                responseModel.DataLst = new List<RoomModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region RoomById
        public async Task<RoomResponseModel> GetRoomById(int roomId)
        {
            var responseModel = new RoomResponseModel();
            try
            {
                var room = await _appDbContext
                    .TblRooms
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.RoomId == roomId);
                if (room is null)
                {
                    responseModel.MessageResponse = new MessageResponseModel
                        (false, EnumStatus.NotFound.ToString());
                    goto result;
                }

                responseModel.Data = room.Change();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.Data = new RoomModel();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

        result:
            return responseModel;
        }
        #endregion

        #region Room Create
        public async Task<MessageResponseModel> CreateRoom(RoomModel requestModel)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                await _appDbContext.TblRooms.AddAsync(requestModel.Change());
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

        #region Room Update
        public async Task<MessageResponseModel> UpdateRoom(int id, RoomModel requestModel)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                var room = await _appDbContext.TblRooms.FirstOrDefaultAsync(x => x.RoomId == id);
                if (room is null)
                {
                    responseModel = new MessageResponseModel(false, EnumStatus.NotFound.ToString());
                    return responseModel;
                }

                #region Patch Method Validation Conditions

                if (!string.IsNullOrEmpty(requestModel.RoomNo))
                {
                    room.RoomNo = requestModel.RoomNo;
                }
                if (requestModel.Price != 0) // or another condition that signifies a valid price
                {
                    room.Price = requestModel.Price;
                }
                if (!string.IsNullOrEmpty(requestModel.facilities))
                {
                    room.facilities = requestModel.facilities;
                }
                if (!string.IsNullOrEmpty(requestModel.Status))
                {
                    room.Status = requestModel.Status;
                }
                if (!string.IsNullOrEmpty(requestModel.Image))
                {
                    room.Image = requestModel.Image;
                }
                #endregion

                _appDbContext.Entry(room).State = EntityState.Modified;
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

        #region Room Delete
        public async Task<MessageResponseModel> DeleteRoom(int id)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                var room = await _appDbContext
                    .TblRooms
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.RoomId == id);
                if (room is null)
                {
                    responseModel = new MessageResponseModel(false, EnumStatus.NotFound.ToString());
                    goto result;
                }

                _appDbContext.TblRooms.Remove(room);
                _appDbContext.Entry(room).State = EntityState.Deleted;
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

        #region Check Room Availability
        public async Task<RoomListResponseModel> CheckRoomAvailability(DateTime checkInDate, DateTime checkOutDate,int roomCategoryId)
        {
            if(checkInDate >= checkOutDate)
            {
                throw new ArgumentException("Invalid Check-in/Check Out dates");
            }
            var responseModel = new RoomListResponseModel();
            try
            {
                var query =  _appDbContext.TblRooms
                            .Where(room => room.Status == "Available"
                                || room.RoomCategoryId == roomCategoryId)
                            .Where(room => !_appDbContext.TblBookings
                            .Any(b => b.RoomId == room.RoomId &&
                            b.CheckInDate < checkOutDate &&
                            b.CheckOutDate > checkInDate));

                responseModel.DataLst=await query
                    .Select(x=>x.Change())
                    .ToListAsync();
                    responseModel.MessageResponse=new MessageResponseModel(true, EnumStatus.Success.ToString());

            }
            catch (Exception ex)
            {
                responseModel.DataLst = new List<RoomModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }



        #endregion

    }

}
