using HotelBookingSystem.Model.Setup.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Setup.RoomCategory
{
    public class RoomCategoryResponseModel
    {
        public RoomCategoryModel Data { get; set; }
        public MessageResponseModel MessageResponse { get; set; }
    }
}
