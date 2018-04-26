using System;

namespace ConvenientShop.API.Models
{
    public class CustomerForOperationsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public int TypeId { get; set; }
    }
}