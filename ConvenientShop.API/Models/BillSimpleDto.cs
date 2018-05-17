using System;

namespace ConvenientShop.API.Models
{
    public class BillSimpleDto
    {
        public int BillId { get; set; }
        public string StaffName { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}