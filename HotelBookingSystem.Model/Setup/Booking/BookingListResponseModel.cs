using HotelBookingSystem.Model.Setup.PageSetting;
using HotelBookingSystem.Model.Setup.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Setup.Booking
{
    public class BookingListResponseModel
    {
        public List<BookingModel> DataLst { get; set; }
        public MessageResponseModel MessageResponse { get; set; }
        public PageSettingModel PageSetting { get; set; }
    }
}
