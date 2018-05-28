using System;

namespace ConvenientShop.API.Models
{
    public class StaffSimpleDto
    {
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
    }
}