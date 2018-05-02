using System;
using System.Collections.Generic;

namespace ConvenientShop.API.Models
{
    public class BillDto
    {
        public string StaffName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<BillDetailDto> BillDetails { get; set; }
    }
}