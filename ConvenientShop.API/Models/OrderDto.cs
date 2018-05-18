using System;

namespace ConvenientShop.API.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string StaffName { get; set; }
    }
}