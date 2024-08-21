﻿using DotNet8.PosBackendApi.Models.Setup.Token;
using HotelBookingSystem.Model.Setup.Login;

namespace HotelBookingSystemApi.Feature.Authentication.Login
{
    public class BL_Login
    {
        private readonly DL_Login _DL_login;

        public BL_Login(DL_Login login) => _DL_login = login;

        public async Task<TokenResponseModel> Login(LoginRequestModel reqModel)
        {

            if (reqModel is null) throw new Exception("UserName and password cannot be null");
            var model = await _DL_login.Login(reqModel);
            return model;
        }
    }
}
