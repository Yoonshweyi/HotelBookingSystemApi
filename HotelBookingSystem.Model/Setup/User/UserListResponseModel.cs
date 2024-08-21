using HotelBookingSystem.Model.Setup.PageSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Setup.User
{
    public class UserListResponseModel
    {
        public List<UserModel> DataLst { get; set; }
        public MessageResponseModel MessageResponse { get; set; }
        public PageSettingModel PageSetting { get; set; }
    }
}
