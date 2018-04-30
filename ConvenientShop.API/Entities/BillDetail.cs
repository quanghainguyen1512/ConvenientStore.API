using System.Collections.Generic;

namespace ConvenientShop.API.Entities
{
    public class BillDetail
    {
        public int BillDetailId { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public int Quantity { get; set; }
        public Bill Bill { get; set; }
    }
}