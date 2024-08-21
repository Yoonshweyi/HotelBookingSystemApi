using DotNet8.PosBackendApi.Models.Setup.Token;
using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Feature.User;
using HotelBookingSystem.Model.Setup.PageSetting;
using HotelBookingSystem.Model.Setup.User;
using HotelBookingSystem.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelBookingSystemApi.Feature.User
{
    public class DL_User
    {
        private readonly AppDbContext _appDbContext;
        private readonly JwtModel _tokenModel;

        public DL_User(IOptionsMonitor<JwtModel> tokenModel,AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _tokenModel = tokenModel.CurrentValue;
        }

        #region Get User
        public async Task<UserListResponseModel> GetUsers()
        {
            var responseModel = new UserListResponseModel();
            try
            {
                var users = await _appDbContext
                    .TblUsers
                    .AsNoTracking()
                    .ToListAsync();
                responseModel.DataLst = users
                    .Select(x => x.Change())
                    .ToList();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.DataLst = new List<UserModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion


        #region Get User With Pagination
        public async Task<UserListResponseModel> GetUsers(int PageSize, int PageNo)
        {
            var responseModel = new UserListResponseModel();
            try
            {
                var userList = _appDbContext.TblUsers.AsNoTracking();


                var user = await userList
                    .Pagination(PageNo, PageSize)
                    .ToListAsync();

                var totalCount = await userList.CountAsync();
                var pageCount = totalCount / PageSize;
                if (totalCount % PageSize > 0)
                    pageCount++;

                responseModel.DataLst = user.Select(x => x.Change()).ToList();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
                responseModel.PageSetting = new PageSettingModel(PageNo, PageSize, pageCount, totalCount);
            }
            catch (Exception ex)
            {
                responseModel.DataLst = new List<UserModel>();
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
        }
        #endregion

        #region GetUserByUserId
         public async Task<UserResponseModel> GetUser(int userId)
         {
            var responseModel = new UserResponseModel();
            try
            {
                 var user = await _appDbContext
                .TblUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);
                 responseModel.Data = user is not null ? user.Change() : new UserModel();
                responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
            }
            catch (Exception ex)
            {
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

            return responseModel;
            }
        #endregion


        #region Create User
        public async Task<MessageResponseModel> CreateUser(UserModel requestModel)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                requestModel.Password = requestModel.Password.ToHash(_tokenModel.Key);
                await _appDbContext.TblUsers.AddAsync(requestModel.Change());
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

        #region Update User
        public async Task<MessageResponseModel> UpdateUser(int id, UserModel requestModel)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                var user = await _appDbContext.TblUsers.FirstOrDefaultAsync(x => x.UserId == id);

                if (user is null)
                {
                    responseModel = new MessageResponseModel(false, EnumStatus.NotFound.ToString());
                    return responseModel;
                }

                #region Patch Method Validation Conditions

                if (!string.IsNullOrEmpty(requestModel.Name))
                {
                    user.Name = requestModel.Name;
                }

                if (!string.IsNullOrEmpty(requestModel.PhoneNo))
                {
                    user.PhoneNo = requestModel.PhoneNo;
                }

                if (!string.IsNullOrEmpty(requestModel.Password))
                {
                    user.Password = requestModel.Password;
                }

                if (!string.IsNullOrEmpty(requestModel.Address))
                {
                    user.Address = requestModel.Address;
                }

                if (!string.IsNullOrEmpty(requestModel.Gender))
                {
                    user.Gender = requestModel.Gender;
                }

                if (!string.IsNullOrEmpty(requestModel.Role))
                {
                    user.Role = requestModel.Role;
                }
                #endregion

               _appDbContext.Entry(user).State = EntityState.Modified;
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

        #region Delete User
        public async Task<MessageResponseModel> DeleteUser(int id)
        {
            var responseModel = new MessageResponseModel();
            try
            {
                var user = await _appDbContext
                    .TblUsers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId== id);
                if (user is null)
                {
                    responseModel = new MessageResponseModel(false, EnumStatus.NotFound.ToString());
                    goto result;
                }

                _appDbContext.TblUsers.Remove(user);
                _appDbContext.Entry(user).State = EntityState.Deleted;
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

        #region Search User
        
        public async Task<UserListResponseModel> SearchUser(string query)
        {
            var responseModel = new UserListResponseModel();
            try
            {
                var normalizedQuery = query.Trim().ToLower();
                
                var queryResult = await _appDbContext.TblUsers.
                        Where(x => x.Name.ToLower().Contains(normalizedQuery) || x.PhoneNo.Contains(query))
                        .ToListAsync();

                responseModel.DataLst = queryResult
                    .Select(x => x.Change())
                    .ToList();
               responseModel.MessageResponse = new MessageResponseModel(true, EnumStatus.Success.ToString());
 
            }
            catch (Exception ex)
            {
                responseModel.MessageResponse = new MessageResponseModel(false, ex);
            }

                return responseModel;
        }

     #endregion

        }
}
