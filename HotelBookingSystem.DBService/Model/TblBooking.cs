using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DBService.Model
{
    public class TblBooking
    {
        public int BookingId { get; set; }
        public int UserId {  get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Decimal TotalAmount {  get; set; }
        public string PaymentStatus {  get; set; }

        public DateTime? PaymentDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedAt { get; set; }



    }
}
