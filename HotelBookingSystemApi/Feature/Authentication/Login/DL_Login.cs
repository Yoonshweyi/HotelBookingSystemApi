using DotNet8.PosBackendApi.Models.Setup.Token;
using HotelBookingSystem.DBService.Model;
using HotelBookingSystem.Model;
using HotelBookingSystem.Model.Setup.Login;
using HotelBookingSystem.Model.Setup.User;
using HotelBookingSystem.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelBookingSystemApi.Feature.Authentication.Login
{
    public class DL_Login
    {
        private readonly JwtTokenGenerate _tokenGenerator;
        private readonly AppDbContext _context;
        private readonly JwtModel _tokenModel;

        public DL_Login(JwtTokenGenerate tokenGenerator,
       AppDbContext context, IOptionsMonitor<JwtModel> tokenModel)
        {
            _tokenGenerator = tokenGenerator;
            _context = context;
            _tokenModel = tokenModel.CurrentValue;
        }

        public async Task<TokenResponseModel> Login(LoginRequestModel reqModel)
        {
            var responseModel = new TokenResponseModel();
            var user=await _context.TblUsers.AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Name==reqModel.UserName);

            if (user is null || user.Password != reqModel.Password.ToHash(_tokenModel.Key))
            {
                responseModel = new TokenResponseModel
                {
                    Message = new MessageResponseModel(false, "UserName and Password Incorrect.Please try again."),
                };
                goto result;
            }
            
            var model = new UserModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Gender = user.Gender,
                Password = user.Password,
                PhoneNo = user.PhoneNo,
                Address = user.Address,
                Role = user.Role
            };
            //if (model.Role == null)
            //{
            //    Console.WriteLine("Role is null before token generation.");
            //}
            //else
            //{
            //    Console.WriteLine($"Role before token generation: '{model.Role}'");
            //}

            var accessToken = _tokenGenerator.GenerateAccessToken(model);
            responseModel = new TokenResponseModel
            {
                Message = new MessageResponseModel(true, "Login Successful"),
                AccessToken = accessToken,
            };
        result:
            return responseModel;
        }
    }
}
