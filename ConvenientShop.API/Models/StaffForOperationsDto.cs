using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Models
{
    public class StaffForOperationsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }    // 1: male - 0: female
        public string PhoneNumber { get; set; }
    }
}
