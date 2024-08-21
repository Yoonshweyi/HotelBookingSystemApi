using DotNet8.PosBackendApi.Models.Setup.Token;
using HotelBookingSystem.Model.Setup.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace HotelBookingSystem.Shared
{
    public class JwtTokenGenerate
    {
        private readonly JwtModel _token;

        public JwtTokenGenerate(IOptionsMonitor<JwtModel> token)
        {
            _token = token.CurrentValue;
        }

        public string GenerateAccessToken(UserModel user)
        {
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _token.Key;
            var key = Encoding.ASCII.GetBytes(secret);

            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   // new Claim("Name", user.Name),
                   new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                  // new Claim(ClaimTypes.Role, "Admin"),
                   //new Claim(ClaimTypes.Role,"User")
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshTokenV1()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        //public string GenerateRefreshToken(string token, string v)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var decodedToken = handler.ReadJwtToken(token);

        //    var item = decodedToken.Claims.FirstOrDefault(x => x.Type == "TokenExpired");
        //    DateTime tokenExpired = Convert.ToDateTime(item?.Value);
        //    var Name = decodedToken.Claims.FirstOrDefault(x => x.Type == "Name") ??
        //                    throw new Exception("Name is required");
        //    var model = new UserModel
        //    {
        //       // UserId = Convert.ToInt32(userId.Value),
        //        Name = Name.Value,
        //        //PhoneNo = PhoneNo.Value,
        //     //  Role = role
        //    };
        //    var refreshToken = DateTime.Now > tokenExpired ? GenerateAccessToken(model) : token;
        //    return refreshToken;
        //}

      

        public string GenerateAccessTokenFromRefreshToken(string refreshToken, string secret)
        {
            // Implement logic to generate a new access token from the refresh token
            // Verify the refresh token and extract necessary information (e.g., user ID)
            // Then generate a new access token

            // For demonstration purposes, return a new token with an extended expiry
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(15), // Extend expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
