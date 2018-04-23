using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Models
{
    public class StaffDto
    {
        public string StaffName { get; set; }
        public string IdentityNumber { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }

    }
}
