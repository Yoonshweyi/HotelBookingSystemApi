using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Setup.User
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string Password { get; set; }
        public string? Address { get; set; }

        public String? Gender { get; set; }

        public string Role { get; set; }
    }
}
