using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("staff")]
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; } // 1: male - 0: female
        public string PhoneNumber { get; set; }
        public int AccountId { get; set; } = -1;
        public string ImageUrl { get; set; }

        [Write(false)]
        public ICollection<Bill> Bills { get; set; }

        [Write(false)]
        public Role Role { get; set; }
    }
}