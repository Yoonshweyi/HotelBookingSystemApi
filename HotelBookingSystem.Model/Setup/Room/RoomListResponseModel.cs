using HotelBookingSystem.Model.Setup.PageSetting;
using HotelBookingSystem.Model.Setup.RoomCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Setup.Room
{
    public class RoomListResponseModel
    {
        public List<RoomModel> DataLst { get; set; }
        public MessageResponseModel MessageResponse { get; set; }
        public PageSettingModel PageSetting { get; set; }
    }
}
