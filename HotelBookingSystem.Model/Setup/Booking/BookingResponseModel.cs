using HotelBookingSystem.Model.Setup.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Setup.Booking
{
    public class BookingResponseModel
    {
        public BookingModel Data { get; set; }
        public MessageResponseModel MessageResponse { get; set; }
    }
}
