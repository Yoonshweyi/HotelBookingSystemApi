using DotNet8.PosBackendApi.Models.Setup.Token;
using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model.Setup.User;
using HotelBookingSystem.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using HotelBookingSystem.Model.Setup.RoomCategory;
using HotelBookingSystem.Model.Setup.PageSetting;
using HotelBookingSystem.Shared;
using HotelBookingSystem.Model.Setup.Room;

namespace HotelBookingSystemApi.Feature.RoomCategory
{
    public class DL_RoomCategory
    {
        private readonly AppDbContext _appDbContext;
        public DL_RoomCategory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Get RoomCategory
        public async Task<RoomCategoryListResponseModel> GetRoomCategory()
        {
            var responseModel = new RoomCategoryListResponseModel();
            try
            {
                var roomCategory = await _appDbContext
                    .TblRoomCategories
                    .AsNoTracking()
                    .ToListAsync();
                responseModel.DataLst = roomCategory
                    .Select(x => x.Change())
                    .ToList();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.DataLst = new List<RoomCategoryModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region Get RoomCategory With Pagination
        public async Task<RoomCategoryListResponseModel> GetRoomCategories(int PageSize, int PageNo)
        {
            var responseModel = new RoomCategoryListResponseModel();
            try
            {
                var roomList = _appDbContext.TblRoomCategories.AsNoTracking();


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
                responseModel.DataLst = new List<RoomCategoryModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region RoomCategoryById
        public async Task<RoomCategoryResponseModel> GetRoomCategoryById(int CategoryId)
        {
            var responseModel = new RoomCategoryResponseModel();
            try
            {
                var roomCategory = await _appDbContext
                    .TblRoomCategories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == CategoryId);
                if (roomCategory is null)
                {
                    responseModel.MessageResponse = new MessageResponseModel
                        (false, EnumStatus.NotFound.ToString());
                    goto result;
                }

                responseModel.Data = roomCategory.Change();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.Data = new RoomCategoryModel();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

        result:
            return responseModel;
        }
        #endregion

        #region RoomCategory Create
        public async Task<MessageResponseModel> CreateRoomCategory(RoomCategoryModel requestModel)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                await _appDbContext.TblRoomCategories.AddAsync(requestModel.Change());
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

        #region RoomCategory Update
        public async Task<MessageResponseModel> UpdateRoomCategory(int id, RoomCategoryModel requestModel)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                //var user = await _appDbContext.TblUsers.FirstOrDefaultAsync(x => x.UserId == id);
                var roomCategory=await _appDbContext.TblRoomCategories.FirstOrDefaultAsync(x => x.Id == id);
                if (roomCategory is null)
                {
                    responseModel = new MessageResponseModel(false, EnumStatus.NotFound.ToString());
                    return responseModel;
                }

                #region Patch Method Validation Conditions

                if (!string.IsNullOrEmpty(requestModel.RoomType))
                {
                    roomCategory.RoomType = requestModel.RoomType;
                }
                #endregion

                _appDbContext.Entry(roomCategory).State = EntityState.Modified;
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

        #region RoomCategory Delete
        public async Task<MessageResponseModel> DeleteRoomCategory(int id)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                var roomCategory = await _appDbContext
                    .TblRoomCategories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (roomCategory is null)
                {
                    responseModel = new MessageResponseModel(false, EnumStatus.NotFound.ToString());
                    goto result;
                }

                _appDbContext.TblRoomCategories.Remove(roomCategory);
                _appDbContext.Entry(roomCategory).State = EntityState.Deleted;
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
