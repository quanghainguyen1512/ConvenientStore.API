using System;

namespace ConvenientShop.API.Models
{
    public class BillForStaffDto
    {
        public string StaffName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int TotalPrice { get; set; }
    }
}