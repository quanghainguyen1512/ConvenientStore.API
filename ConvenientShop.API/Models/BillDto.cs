using System;
using System.Collections.Generic;
using System.Linq;

namespace ConvenientShop.API.Models
{
    public class BillDto
    {
        public int BillId { get; set; }
        public string StaffName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int TotalPrice => BillDetails.Sum(bd => bd.SubTotal);
        // public string CustomerName { get; set; }
        public IEnumerable<BillDetailDto> BillDetails { get; set; }
    }
}