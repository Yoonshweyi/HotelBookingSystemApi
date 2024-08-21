using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.Room;
using HotelBookingSystem.Model.Setup.RoomCategory;
using HotelBookingSystem.Model.Setup.User;
using HotelBookingSystemApi.Feature.RoomCategory;

namespace HotelBookingSystemApi.Feature.Room
{
    public class BL_Room
    {
        private readonly DL_Room _dL_Room;
        public BL_Room(DL_Room dL_Room) =>_dL_Room = dL_Room;

        #region Get Room
        public async Task<RoomListResponseModel> GetRoom()
        {
            var response = await _dL_Room.GetRoom();
            return response;
        }
        #endregion

        #region  Get Room With Pagination
        public async Task<RoomListResponseModel> GetRoom(int PageSize, int PageNo)
        {
            if (PageSize <= 0) throw new Exception("PageSize is not less than 0.");
            if (PageNo <= 0) throw new Exception("PageNo is not less than 0.");
            var model = await _dL_Room.GetRoom(PageSize, PageNo);
            return model;
        }
        #endregion

        #region Get RoomById
        public async Task<RoomResponseModel> GetRoomById(int roomId)
        {
            if (roomId == null) throw new Exception("RoomId is null");
            var response = await _dL_Room.GetRoomById(roomId);
            return response;
        }
        #endregion

        #region Create Room
        public async Task<MessageResponseModel> CreateRoom(RoomModel requestModel)
        {
            CheckRoomNullValue(requestModel);
            var response = await _dL_Room.CreateRoom(requestModel);
            return response;
        }
        #endregion

        #region Update Room
        public async Task<MessageResponseModel> UpdateRoom(int id, RoomModel requestModel)
        {
            if (id == 0) throw new Exception("RoomId is 0.");
            CheckRoomNullValue(requestModel);
            var model = await _dL_Room.UpdateRoom(id, requestModel);
            return model;
        }
        #endregion

        #region Delete Room
        public async Task<MessageResponseModel> DeleteRoom(int id)
        {
            if (id == 0) throw new Exception("id is 0.");
            var model = await _dL_Room.DeleteRoom(id);
            return model;
        }
        #endregion

        #region Check Room Availability
        public async Task<RoomListResponseModel> CheckRoomAvailability(DateTime CheckInDate,DateTime CheckOutDate,int RoomCategoryId)
        {
            var response = await _dL_Room.CheckRoomAvailability(CheckInDate,CheckOutDate,RoomCategoryId);
            return response;
        }
        #endregion

        private void CheckRoomNullValue(RoomModel requestModel)
        {
            if (requestModel == null)
            {
                throw new Exception("RoomModel is null.");
            }

            if (string.IsNullOrWhiteSpace(requestModel.RoomNo))
            {
                throw new Exception("RoomNo is null.");
            }
            if(requestModel.Price == 0)
            {
                throw new Exception("Price is null");
            }
            if (string.IsNullOrWhiteSpace(requestModel.facilities))
            {
                throw new Exception("facilities is null.");
            }
            if (string.IsNullOrWhiteSpace(requestModel.Status))
            {
                throw new Exception("Status is null.");
            }
            if (string.IsNullOrWhiteSpace(requestModel.Image))
            {
                throw new Exception("Image is null.");
            }
           


    }

}
}
