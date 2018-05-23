using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Models
{
    public class StaffDto : StaffSimpleDto
    {
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }

    }
}