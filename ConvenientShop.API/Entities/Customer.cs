using System;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("customer")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public CustomerType CustomerType { get; set; }
    }
}