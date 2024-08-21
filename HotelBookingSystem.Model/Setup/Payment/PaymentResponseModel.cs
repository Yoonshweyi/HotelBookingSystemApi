using HotelBookingSystem.Model.Setup.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Setup.Payment
{
    public class PaymentResponseModel
    {
        public PaymentModel Data { get; set; }
        public MessageResponseModel MessageResponse { get; set; }
    }
}
