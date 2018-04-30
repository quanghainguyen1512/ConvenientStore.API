using System;
using System.Collections.Generic;

namespace ConvenientShop.API.Entities
{
    public class Bill
    {
        public int BillId { get; set; }
        public Staff Staff { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int TotalPrice { get; set; }
        public Customer Customer { get; set; }
        public ICollection<BillDetail> BillDetails { get; set; }
    }
}