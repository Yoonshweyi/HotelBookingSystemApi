using HotelBookingSystem.Model.Setup.RoomCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Setup.Room
{
    public class RoomResponseModel
    {
        public RoomModel Data { get; set; }
        public MessageResponseModel MessageResponse { get; set; }
    }
}
