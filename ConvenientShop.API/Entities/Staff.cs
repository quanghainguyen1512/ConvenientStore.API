using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Entities
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; } // 1: male - 0: female
        public string PhoneNumber { get; set; }
        public int AccountId { get; set; }
        public ICollection<Bill> Bills { get; set; }
    }
}