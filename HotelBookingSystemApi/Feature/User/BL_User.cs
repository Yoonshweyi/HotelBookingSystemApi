
using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Feature.User;
using HotelBookingSystem.Model.Setup.User;

namespace HotelBookingSystemApi.Feature.User
{
    public class BL_User
    {
        private readonly DL_User _dL_User;
        public BL_User(DL_User dL_User) => _dL_User = dL_User;

        #region Get User
        public async Task<UserListResponseModel> GetUsers()
        {
            var response = await _dL_User.GetUsers();
            return response;
        }
        #endregion
        #region  Get User With Pagination
        public async Task<UserListResponseModel> GetUsers(int PageSize, int PageNo)
        {
            if (PageSize <= 0) throw new Exception("PageSize is not less than 0.");
            if (PageNo <= 0) throw new Exception("PageNo is not less than 0.");
            var model = await _dL_User.GetUsers(PageSize, PageNo);
            return model;
        }
        #endregion

        #region GetUser By UserId
        public async Task<UserResponseModel> GetUser(int userId)
        {
            var item = await _dL_User.GetUser(userId);
            return item;
        }
        #endregion
        #region Create User
        public async Task<MessageResponseModel> CreateUser(UserModel requestModel)
        {
            CheckUserNullValue(requestModel);
            var response = await _dL_User.CreateUser(requestModel);
            return response;
        }
        #endregion

        #region Update User
        public async Task<MessageResponseModel> UpdateUser(int userId, UserModel requestModel)
        {
            if (userId == 0) throw new Exception("userId is 0.");
           // CheckShopNullValue(requestModel);
            var model = await _dL_User.UpdateUser(userId, requestModel);
            return model;
        }
        #endregion

        #region delete user
        public async Task<MessageResponseModel> DeleteUser(int id)
        {
            if (id == 0) throw new Exception("id is 0.");
            var model = await _dL_User.DeleteUser(id);
            return model;
        }
        #endregion

        #region Search User
        public async Task<UserListResponseModel> SearchUser(string reqData)
        {
            var response = await _dL_User.SearchUser(reqData);
            return response;
        }
        #endregion
        private void CheckUserNullValue(UserModel requestModel)
        {
            if (requestModel == null)
            {
                throw new Exception("UserModel is null.");
            }

            if (string.IsNullOrWhiteSpace(requestModel.Name))
            {
                throw new Exception("UserName is null.");
            }

            if (string.IsNullOrWhiteSpace(requestModel.PhoneNo))
            {
                throw new Exception("PhoneNo is null.");
            }
            if (string.IsNullOrWhiteSpace(requestModel.Password))
            {
                throw new Exception("Password is null.");
            }
            if (string.IsNullOrWhiteSpace(requestModel.Address))
            {
                throw new Exception("Address is null.");
            }
            if (string.IsNullOrWhiteSpace(requestModel.Gender))
            {
                throw new Exception("Gender is null.");
            }
            if (string.IsNullOrWhiteSpace(requestModel.Role))
            {
                throw new Exception("Role is null.");
            }
        }


    }
}
