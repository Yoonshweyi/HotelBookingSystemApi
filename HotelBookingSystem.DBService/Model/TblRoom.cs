using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DBService.Model
{
    public class TblRoom
    {
      public int RoomId { get; set; }
      public string RoomNo { get; set; }
      public decimal Price { get; set; }
      public string facilities { get; set; }
      public string Status { get; set; }
      public string? Image {  get; set; }
      public int RoomCategoryId { get; set; }
    

    }

    //public enum StatusEnum
    //{
    //    Available,
    //    Occupied
    //}
}
