using HotelBookingSystem.Model.Setup.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Feature.User
{
    public class UserResponseModel
    {
        public UserModel Data { get; set; }
        public MessageResponseModel MessageResponse { get; set; }
    }
}
