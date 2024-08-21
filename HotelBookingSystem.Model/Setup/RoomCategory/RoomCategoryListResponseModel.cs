using HotelBookingSystem.Model.Setup.PageSetting;
using HotelBookingSystem.Model.Setup.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Setup.RoomCategory
{
    public class RoomCategoryListResponseModel
    {
        public List<RoomCategoryModel> DataLst {  get; set; }
        public MessageResponseModel MessageResponse { get; set; }
        public PageSettingModel PageSetting { get; set; }
    }
}
