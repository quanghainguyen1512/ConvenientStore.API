using System;

namespace ConvenientShop.API.Models
{
    public class BillForRevenueDto
    {
        public int BillId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int SumPerBill { get; set; }
    }
}