using System;

namespace ConvenientShop.API.Entities
{
    public class Bill
    {
        public Staff Staff { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int TotalPrice { get; set; }
        public Customer Customer { get; set; }
    }
}