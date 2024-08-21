using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.RoomCategory;
using HotelBookingSystem.Model.Setup.User;
using HotelBookingSystemApi.Feature.User;

namespace HotelBookingSystemApi.Feature.RoomCategory
{
    public class BL_RoomCategory
    {
        private readonly DL_RoomCategory _dL_roomCategory;
        public BL_RoomCategory(DL_RoomCategory dL_roomCategory)=>
            _dL_roomCategory=dL_roomCategory;


        #region Get RoomCategory
        public async Task<RoomCategoryListResponseModel> GetRoomCategory()
        {
            var response=await _dL_roomCategory.GetRoomCategory();
            return response;
        }
        #endregion
        #region  Get RoomCategory With Pagination
        public async Task<RoomCategoryListResponseModel> GetRoomCategories(int PageSize, int PageNo)
        {
            if (PageSize <= 0) throw new Exception("PageSize is not less than 0.");
            if (PageNo <= 0) throw new Exception("PageNo is not less than 0.");
            var model = await _dL_roomCategory.GetRoomCategories(PageSize, PageNo);
            return model;
        }
        #endregion
        #region Get RoomCategoryById
        public async Task<RoomCategoryResponseModel> GetRoomCategoryById(int CategoryId)
        {
            if (CategoryId==null) throw new Exception("CategoryId is null");
            var response = await _dL_roomCategory.GetRoomCategoryById(CategoryId);
            return response;
        }
        #endregion

        #region Create RoomCategory
        public async Task<MessageResponseModel> CreateRoomCategory(RoomCategoryModel requestModel)
        {
            CheckRoomCategoryNullValue(requestModel);
            var response = await _dL_roomCategory.CreateRoomCategory(requestModel);
            return response;
        }
        #endregion

        #region Update RoomCategory
        public async Task<MessageResponseModel> UpdateRoomCategory(int id, RoomCategoryModel requestModel)
        {
            if (id == 0) throw new Exception("CategoryId is 0.");
             CheckRoomCategoryNullValue(requestModel);
            var model = await _dL_roomCategory.UpdateRoomCategory(id, requestModel);
            return model;
        }
        #endregion
        #region Delete RoomCategory
        public async Task<MessageResponseModel> DeleteRoomCategory(int id)
        {
            if (id == 0) throw new Exception("id is 0.");
            var model = await _dL_roomCategory.DeleteRoomCategory(id);
            return model;
        }
        #endregion

        private void CheckRoomCategoryNullValue(RoomCategoryModel roomCategory)
        {
            if (roomCategory is null)
            {
                throw new Exception("RoomCategory is null.");
            }
            if (string.IsNullOrWhiteSpace(roomCategory.RoomType))
            {
                throw new Exception("RoomType is null.");
            }

        }


    }
    }
