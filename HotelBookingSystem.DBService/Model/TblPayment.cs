using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DBService.Model

{
    public class TblPayment
    {
        public int PaymentId {  get; set; }
        public int BookingId {  get; set; }
        public string PaymentMethod {  get; set; }
        public decimal Amount {  get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
